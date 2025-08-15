using HahnSoftware.Domain.Models;

namespace HahnSoftware.Domain.Interfaces.Services;

public interface IMailService
{
    Task SendAsync(string to, string subject, string body);
    Task SendAsync(MailVM model);
}
