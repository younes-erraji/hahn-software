using Microsoft.Extensions.Configuration;

using System.Net.Mail;
using System.Net.Mime;
using System.Net;

using HahnSoftware.Application.Extensions;
using HahnSoftware.Domain.Interfaces.Services;
using HahnSoftware.Domain.Models;

namespace HahnSoftware.Infrastructure.Services;

public class MailService : IMailService
{
    private readonly int _smtpPort;
    private readonly string _smtpServer;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;
    private readonly string _fromEmail;
    private readonly string _fromName;

    public MailService(IConfiguration configuration)
    {
        _smtpServer = configuration.GetConfig("Mail:SmtpServer");
        _smtpPort = int.Parse(configuration.GetConfig("Mail:SmtpPort"));
        _smtpUsername = configuration.GetConfig("Mail:Username");
        _smtpPassword = configuration.GetConfig("Mail:Password");
        _fromEmail = configuration.GetConfig("Mail:FromEmail");
        _fromName = configuration.GetConfig("Mail:FromName");
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        MailMessage message = new MailMessage
        {
            From = new MailAddress(_fromEmail, _fromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(to);

        using (SmtpClient client = new SmtpClient(_smtpServer, _smtpPort))
        {
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

            await client.SendMailAsync(message);
        }
    }

    public async Task SendAsync(MailVM model)
    {
        using (MailMessage message = new MailMessage())
        {
            message.IsBodyHtml = true;
            message.From = new MailAddress(_fromEmail, _fromName);
            message.To.Add(new MailAddress(model.ToEmail, model.ToName));
            message.Subject = model.Subject;
            message.Body = model.Body;

            if (model.CarbonCopies.IsNotEmpty())
            {
                foreach (string cc in model.CarbonCopies)
                {
                    message.CC.Add(cc);
                }
            }

            if (model.Attachments.IsNotEmpty())
            {
                foreach (MailAttachmentVM mailAttachment in model.Attachments)
                {
                    Attachment attachment = new Attachment(mailAttachment.Stream, mailAttachment.Filename);
                    ContentDisposition? disposition = attachment.ContentDisposition;
                    if (disposition == null)
                    {
                        return;
                    }

                    disposition.DispositionType = "attachment";

                    message.Attachments.Add(attachment);
                }
            }

            using (SmtpClient client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

                await client.SendMailAsync(message);
            }
        }
    }
}
