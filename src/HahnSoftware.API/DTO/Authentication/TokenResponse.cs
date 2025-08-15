namespace HahnSoftware.API.DTO.Authentication;

public class TokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}