using HahnSoftware.Application.RESTful;

using MediatR;

namespace HahnSoftware.Application.Authentication.Commands.MailVerification;

public class MailVerificationCommand : IRequest<Response>
{
    public string Token { get; set; }
    public string Mail { get; set; }

    public MailVerificationCommand(string token, string mail)
    {
        Token = token;
        Mail = mail;
    }
}
