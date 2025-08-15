using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<Response>
{
    public string Mail { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
    public string PasswordConfirmation { get; set; }

    public ResetPasswordCommand(string mail, string token, string newPassword, string passwordConfirmation)
    {
        Mail = mail;
        Token = token;
        NewPassword = newPassword;
        PasswordConfirmation = passwordConfirmation;
    }
}
