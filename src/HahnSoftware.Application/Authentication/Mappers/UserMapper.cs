using HahnSoftware.API.DTO.Authentication;
using HahnSoftware.Application.Authentication.DTO;
using HahnSoftware.Domain.Entities;

namespace HahnSoftware.Application.Authentication.Mappers;

public static class UserMapper
{
    public static UserDetailDTO MapEntityToDetailDTO(User user, TokenResponse token)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        return new UserDetailDTO
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Mail = user.Mail,
            Token = token
        };
    }
}
