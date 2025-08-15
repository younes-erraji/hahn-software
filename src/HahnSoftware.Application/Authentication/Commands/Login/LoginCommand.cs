using HahnSoftware.Application.RESTful;
using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.Login;

public class LoginCommand : IRequest<Response>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; } = false;

    public LoginCommand(string email, string password, bool rememberMe)
    {
        Email = email;
        Password = password;
        RememberMe = rememberMe;
    }
}
