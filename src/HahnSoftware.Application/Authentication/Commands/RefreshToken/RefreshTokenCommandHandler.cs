using HahnSoftware.Application.Authentication.DTO;
using HahnSoftware.Application.Authentication.Mappers;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IAuthenticationService _authenticationService;

    public RefreshTokenCommandHandler(IUserRepository userRepository,  IAuthenticationService authenticationService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Response> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetActiveRefreshTokenAsync(request.RefreshToken);
        if (user is null)
            return Response.Unauthorized("Invalid refresh token");

        Domain.Entities.RefreshToken? oldRefreshToken = user.RefreshTokens.First(x => x.Token == request.RefreshToken);

        string accessToken = _authenticationService.GenerateAccessToken(user);
        // Domain.Entities.RefreshToken refreshToken = _authenticationService.GenerateRefreshToken(user.Id, false);
        // oldRefreshToken.ReplaceToken(refreshToken.Token);

        /*
        user.RefreshTokens.Add(refreshToken);
        List<Domain.Entities.RefreshToken> tokensToRemove = user.RefreshTokens
            .Where(x => x.Active == false && x.CreationDate.AddDays(2) <= DateTimeOffset.Now)
            .ToList();

        foreach (Domain.Entities.RefreshToken token in tokensToRemove)
        {
            user.RefreshTokens.Remove(token);
        }
        */

        await _refreshTokenRepository.Update(oldRefreshToken);
        // await _refreshTokenRepository.Create(refreshToken);
        await _refreshTokenRepository.SaveChanges();

        TokenResponse token = new()
        {
            AccessToken = accessToken,
            RefreshToken = oldRefreshToken.Token
        };

        return Response.Success(UserMapper.MapEntityToDetailDTO(user, token));
    }
}
