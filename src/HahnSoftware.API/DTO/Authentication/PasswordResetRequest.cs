namespace HahnSoftware.API.DTO.Authentication;

public class PasswordResetRequest
{
    public string Mail { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
    public string PasswordConfirmation { get; set; }
}
