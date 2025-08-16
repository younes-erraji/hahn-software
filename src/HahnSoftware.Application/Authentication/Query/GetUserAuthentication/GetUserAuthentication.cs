using HahnSoftware.API.DTO.Authentication;

using MediatR;

namespace HahnSoftware.Application.Authentication.Query.GetUserAuthentication;

public class GetUserAuthentication : IRequest<UserDetailDTO>
{
}
