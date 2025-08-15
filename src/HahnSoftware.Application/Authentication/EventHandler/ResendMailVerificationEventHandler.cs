using HahnSoftware.Application.Extensions;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Events.Users;
using HahnSoftware.Domain.Interfaces.Services;

using MediatR;

using Microsoft.Extensions.Configuration;

namespace HahnSoftware.Application.Authentication.EventHandler;

public class ResendMailVerificationEventHandler : INotificationHandler<ResendMailVerificationEvent>
{
    private readonly IMailService _mailService;
    private readonly string _mailVerificationUrl;
    // private readonly string _resetPasswordUrl;

    public ResendMailVerificationEventHandler(IMailService mainService, IConfiguration configuration)
    {
        _mailService = mainService;
        _mailVerificationUrl = configuration.GetConfig("Mail:VerificationUrl");
        // _resetPasswordUrl = configuration.GetConfig("Mail:ResetPasswordUrl");
    }

    public async Task Handle(ResendMailVerificationEvent notification, CancellationToken cancellationToken)
    {
        string verificationLink = $"{_mailVerificationUrl}?token={notification.VerificationToken}&mail={Uri.EscapeDataString(notification.Mail)}";

        await _mailService.SendAsync(
            notification.Mail,
            "Verify Your Email",
            $"Please verify your email by clicking on this link: <a href='{verificationLink}'>Verify Email</a>"
        );
    }
}
