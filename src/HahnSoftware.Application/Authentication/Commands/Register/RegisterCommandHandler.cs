using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Events.Users;

using MediatR;

using System.Security.Cryptography;

using static BCrypt.Net.BCrypt;

namespace HahnSoftware.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public RegisterCommandHandler(IMediator mediator, IUserRepository userRepository, IAuthenticationService authenticationService)
    {
        _mediator = mediator;
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task<Response> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        string verificationToken = _authenticationService.GenerateToken();
        DateTimeOffset tokenExpiry = DateTimeOffset.UtcNow.AddHours(24);
        string key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        string password = _authenticationService.GetPassword(key, request.Password);
        User user = new User(
            key: key,
            firstName: request.FirstName,
            lastName: request.LastName,
            mail: request.Mail,
            password: HashPassword(password, GenerateSalt()),
            verificationToken: verificationToken,
            tokenExpiry: tokenExpiry
        );

        await _userRepository.Create(user);
        await _userRepository.SaveChanges();

        await _mediator.Publish(new UserRegistrationEvent(user.Id, user.Mail, verificationToken));

        return Response.Success("Registration successful. Please check your email to verify your account.");
    }
}
