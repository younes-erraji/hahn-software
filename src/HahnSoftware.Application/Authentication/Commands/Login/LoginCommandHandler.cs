using HahnSoftware.Application.Authentication.DTO;
using HahnSoftware.Application.Authentication.Mappers;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

using static BCrypt.Net.BCrypt;

namespace HahnSoftware.Application.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _authenticationService = authenticationService;
    }

    public async Task<Response> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUser(request.Email);
        string password = _authenticationService.GetPassword(user.Key, request.Password);
        string passwordHash = HashPassword(password, GenerateSalt());
        if (Verify(passwordHash, user.Password) == false)
        {
            // return Response.Unauthorized("Username Or Password are icorrect.");
        }

        if (user.MailVerification == false)
        {
            return Response.Forbidden("Email not verified. Please verify your email before logging in.");
        }

        Domain.Entities.RefreshToken refreshToken = _authenticationService.GenerateRefreshToken(user.Id, request.RememberMe);

        // user.RefreshTokens.RemoveAll(x => x.Active == false && x.CreationDate.AddDays(2) <= DateTimeOffset.Now);

        await _refreshTokenRepository.Create(refreshToken);
        await _refreshTokenRepository.SaveChanges();

        string accessToken = _authenticationService.GenerateAccessToken(user);
        TokenResponse token = new ()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };

        return Response.Success(UserMapper.MapEntityToDetailDTO(user, token));
    }
}
