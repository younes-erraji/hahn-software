using HahnSoftware.Application.Extensions;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Domain.Events.Users;

using MediatR;

using Microsoft.Extensions.Configuration;

namespace HahnSoftware.Application.Authentication.EventHandler;

public class UserRegistrationEventHandler : INotificationHandler<UserRegistrationEvent>
{
    private readonly IMailService _mailService;
    private readonly string _mailVerificationUrl;
    // private readonly string _resetPasswordUrl;

    public UserRegistrationEventHandler(IMailService mainService, IConfiguration configuration)
    {
        _mailService = mainService;
        _mailVerificationUrl = configuration.GetConfig("Mail:VerificationUrl");
        // _resetPasswordUrl = configuration.GetConfig("Mail:ResetPasswordUrl");
    }

    public async Task Handle(UserRegistrationEvent notification, CancellationToken cancellationToken)
    {
        string verificationLink = $"{_mailVerificationUrl}?token={Uri.EscapeDataString(notification.VerificationToken)}&mail={Uri.EscapeDataString(notification.Mail)}";

        await _mailService.SendAsync(
            notification.Mail,
            "Verify Your Email",
            $"Please verify your email by clicking on this link: <a href='{verificationLink}'>Verify Email</a>"
        );
    }
}
