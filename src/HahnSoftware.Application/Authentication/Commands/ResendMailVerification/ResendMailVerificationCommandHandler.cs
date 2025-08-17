using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Events.Users;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.ResendMailVerification;

public class ResendMailVerificationHandler : IRequestHandler<ResendMailVerificationCommand, Response>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public ResendMailVerificationHandler(IUserRepository userRepository, IAuthenticationService authenticationService, IMediator mediator)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _mediator = mediator;
    }

    public async Task<Response> Handle(ResendMailVerificationCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUser(request.Email);

        if (user.MailVerification)
        {
            return Response.BadRequest("Email is already verified");
        }

        string verificationToken = _authenticationService.GenerateToken();
        DateTimeOffset tokenExpiry = DateTimeOffset.UtcNow.AddHours(24);

        user.ResendMailVerification(verificationToken, tokenExpiry);

        await _userRepository.Update(user);
        await _userRepository.SaveChanges();

        await _mediator.Publish(new ResendMailVerificationEvent(user.Id, user.Mail, verificationToken));

        return Response.Success("Verification email has been resent. Please check your inbox.");
    }
}