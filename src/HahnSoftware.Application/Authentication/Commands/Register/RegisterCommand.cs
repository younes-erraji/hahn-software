using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.Register;

public class RegisterCommand : IRequest<Response>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Mail { get; set; }

    public RegisterCommand(string firstName, string lastName, string password, string mail)
    {
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Mail = mail;
    }
}
