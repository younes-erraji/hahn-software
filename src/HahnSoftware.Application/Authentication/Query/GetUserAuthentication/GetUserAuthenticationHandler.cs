using HahnSoftware.API.DTO.Authentication;
using HahnSoftware.Domain.Interfaces.Repositories;

using MediatR;

namespace HahnSoftware.Application.Authentication.Query.GetUserAuthentication;

public class GetUserAuthenticationHandler : IRequestHandler<GetUserAuthentication, UserDetailDTO>
{
    private readonly IUserRepository _userRepository;

    public GetUserAuthenticationHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDetailDTO> Handle(GetUserAuthentication request, CancellationToken cancellationToken)
    {
        return new UserDetailDTO { };
    }
}