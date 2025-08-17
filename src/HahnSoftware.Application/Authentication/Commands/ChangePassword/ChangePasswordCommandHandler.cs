using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

using static BCrypt.Net.BCrypt;

namespace HahnSoftware.Application.Authentication.Commands.ChangePassword;

public class ChangePasswordCommandValidator : IRequestHandler<ChangePasswordCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserService _userService;

    public ChangePasswordCommandValidator(IUserRepository userRepository, IAuthenticationService authenticationService, IUserService userService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _userService = userService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Response> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userService.GetUserIdentifier();
        User user = await _userRepository.GetUser(userId);
        string password = _authenticationService.GetPassword(user.Key, request.CurrentPassword);
        string passwordHash = HashPassword(password, GenerateSalt());
        if (Verify(passwordHash, user.Password) == false)
        {
            return Response.BadRequest("Current password is incorrect");
        }

        string newPassword = _authenticationService.GetPassword(user.Key, request.NewPassword);

        user.ChangePassword(HashPassword(newPassword, GenerateSalt()));

        foreach (Domain.Entities.RefreshToken token in user.RefreshTokens.Where(x => x.Active))
        {
            token.Revoke("Change Password");
        }

        await _refreshTokenRepository.Update(user.RefreshTokens);
        await _userRepository.Update(user);
        await _userRepository.SaveChanges();

        return Response.Success("Password has been changed successfully");
    }
}
