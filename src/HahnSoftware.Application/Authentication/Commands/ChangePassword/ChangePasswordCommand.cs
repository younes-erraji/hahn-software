using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<Response>
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string NewPasswordConfirmation { get; set; }

    public ChangePasswordCommand(string currentPassword, string newPassword, string newPasswordConfirmation)
    {
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
        NewPasswordConfirmation = newPasswordConfirmation;
    }
}
