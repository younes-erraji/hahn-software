using HahnSoftware.Application.Authentication.DTO;

namespace HahnSoftware.API.DTO.Authentication;

public class UserDetailDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mail { get; set; }
    public string? Avatar { get; set; }

    public TokenResponse Token { get; set; }
}
