using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public ForgotPasswordCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task<Response> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUser(request.Email);

        if (user is null)
        {
            // For security reasons, don't reveal if the user exists or not
            return Response.Success("If your email is registered, you will receive a password reset link.");
        }

        if (user.MailVerification == false)
        {
            return Response.BadRequest("Email is not verified. Please verify your email first.");
        }

        string resetToken = _authenticationService.GenerateToken();
        DateTimeOffset tokenExpiry = DateTimeOffset.UtcNow.AddHours(1);

        user.ForgotPassword(resetToken, tokenExpiry);
        await _userRepository.Update(user);
        await _userRepository.SaveChanges();

        /*
        string resetLink = $"{_resetPasswordUrl}?token={resetToken}&email={Uri.EscapeDataString(user.Mail)}";

        await _mailProvider.SendAsync(
            user.Mail,
            "Reset Your Password",
            $"Please reset your password by clicking on this link: <a href='{resetLink}'>Reset Password</a>. This link is valid for 1 hour."
        );
        */

        return Response.Success("If your email is registered, you will receive a password reset link.");
    }
}
