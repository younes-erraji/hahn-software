using HahnSoftware.Application.Extensions;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Events.Users;
using HahnSoftware.Domain.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace HahnSoftware.Application.Authentication.EventHandler;

public class ForgotPasswordEventHandler : INotificationHandler<ForgotPasswordEvent>
{
    private readonly IMailService _mailService;
    private readonly string _resetPasswordUrl;

    public ForgotPasswordEventHandler(IMailService mainService, IConfiguration configuration)
    {
        _mailService = mainService;
        _resetPasswordUrl = configuration.GetConfig("Mail:ResetPasswordUrl");
    }

    public async Task Handle(ForgotPasswordEvent notification, CancellationToken cancellationToken)
    {
        string resetLink = $"{_resetPasswordUrl}?token={Uri.EscapeDataString(notification.PasswordResetToken)}&email={Uri.EscapeDataString(notification.Mail)}";

        await _mailService.SendAsync(
            notification.Mail,
            "Reset Your Password",
            $"Please reset your password by clicking on this link: <a href='{resetLink}'>Reset Password</a>. This link is valid for 1 hour."
        );
    }
}
