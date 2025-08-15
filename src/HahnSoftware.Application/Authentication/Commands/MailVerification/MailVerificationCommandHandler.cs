using HahnSoftware.Application.Authentication.Commands.Login;
using HahnSoftware.Application.Authentication.DTO;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.MailVerification;

public class MailVerificationCommandHandler : IRequestHandler<MailVerificationCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public MailVerificationCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task<Response> Handle(MailVerificationCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.Mail))
        {
            return Response.BadRequest("Token and email are required");
        }

        User user = await _userRepository.GetUser(request.Mail);

        if (user.MailVerification)
        {
            return Response.BadRequest("Email is already verified");
        }

        if (user.MailVerificationToken != request.Token)
        {
            return Response.BadRequest("Invalid verification token");
        }

        if (user.MailVerificationTokenExpiry < DateTimeOffset.UtcNow)
        {
            return Response.BadRequest("Verification token has expired");
        }

        user.VerifyMail();

        await _userRepository.Update(user);
        await _userRepository.SaveChanges();

        return Response.Success("Email verified successfully. You can now log in.");
    }
}
