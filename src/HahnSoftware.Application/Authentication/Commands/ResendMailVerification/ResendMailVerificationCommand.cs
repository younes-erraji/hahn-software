using HahnSoftware.Application.RESTful;
using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.ResendMailVerification;

public class ResendMailVerificationCommand : IRequest<Response>
{
    public string Email { get; set; }

    public ResendMailVerificationCommand(string email)
    {
        Email = email;
    }
}
