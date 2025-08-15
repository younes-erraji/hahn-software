using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<Response>
{
    public string Email { get; set; }

    public ForgotPasswordCommand(string email)
    {
        Email = email;
    }
}
