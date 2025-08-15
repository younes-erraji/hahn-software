using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.RevokeToken;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RevokeTokenCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Response> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetActiveRefreshTokenAsync(request.RefreshToken);

        if (user is null)
            return Response.NotFound("Active token");

        Domain.Entities.RefreshToken refreshToken = user.RefreshTokens.First(x => x.Token == request.RefreshToken);

        refreshToken.Revoke("Revoked without replacement");
        await _refreshTokenRepository.Update(refreshToken);
        await _refreshTokenRepository.SaveChanges();

        return Response.Success("Token revoked");
    }
}
