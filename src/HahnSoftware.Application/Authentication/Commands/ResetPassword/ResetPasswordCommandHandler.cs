using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

using static BCrypt.Net.BCrypt;

namespace HahnSoftware.Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthenticationService _authenticationService;

    public ResetPasswordCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _authenticationService = authenticationService;
    }

    public async Task<Response> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUser(request.Mail);

        if (user.PasswordResetToken != request.Token)
        {
            return Response.BadRequest("Invalid reset token");
        }

        if (user.PasswordResetTokenExpiry < DateTimeOffset.UtcNow)
        {
            return Response.BadRequest("Reset token has expired");
        }

        string password = _authenticationService.GetPassword(user.Key, request.NewPassword);
        user.ResetPassword(HashPassword(password, GenerateSalt()));

        foreach (Domain.Entities.RefreshToken token in user.RefreshTokens.Where(x => x.Active))
        {
            token.Revoke("Password changed");
            await _refreshTokenRepository.Update(token);
        }

        await _userRepository.Update(user);
        await _userRepository.SaveChanges();

        return Response.Success("Password has been reset successfully. You can now log in with your new password.");
    }
}
