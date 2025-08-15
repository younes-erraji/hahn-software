using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.RevokeToken;

public class RevokeTokenCommand : IRequest<Response>
{
    public string RefreshToken { get; set; }

    public RevokeTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
