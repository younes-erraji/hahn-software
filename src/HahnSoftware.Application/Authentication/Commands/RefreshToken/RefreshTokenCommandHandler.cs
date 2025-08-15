using HahnSoftware.Application.Authentication.DTO;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public RefreshTokenCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task<Response> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetActiveRefreshTokenAsync(request.RefreshToken);
        if (user is null)
            return Response.Unauthorized("Invalid refresh token");

        Domain.Entities.RefreshToken? oldRefreshToken = user.RefreshTokens.First(x => x.Token == request.RefreshToken);

        string accessToken = _authenticationService.GenerateAccessToken(user);
        Domain.Entities.RefreshToken refreshToken = _authenticationService.GenerateRefreshToken(false);

        oldRefreshToken.ReplaceToken(refreshToken.Token);

        user.RefreshTokens.Add(refreshToken);

        List<Domain.Entities.RefreshToken> tokensToRemove = user.RefreshTokens
            .Where(x => x.Active == false && x.CreationDate.AddDays(2) <= DateTimeOffset.Now)
            .ToList();

        foreach (Domain.Entities.RefreshToken token in tokensToRemove)
        {
            user.RefreshTokens.Remove(token);
        }

        await _userRepository.SaveChanges();

        return Response.Success(new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        });
    }
}
