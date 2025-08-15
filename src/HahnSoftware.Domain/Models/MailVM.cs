namespace HahnSoftware.Domain.Models;

public class MailVM
{
    public MailVM(string toName, string toEmail, string subject, string body)
    {
        ToName = toName;
        ToEmail = toEmail;
        Subject = subject;
        Body = body;
    }

    public MailVM(string toName, string toEmail, string subject, string body, ICollection<string> carbonCopies)
    {
        ToName = toName;
        ToEmail = toEmail;
        Subject = subject;
        Body = body;
        CarbonCopies = carbonCopies;
    }
    
    public MailVM(string toName, string toEmail, string subject, string body, ICollection<MailAttachmentVM> attachments)
    {
        ToName = toName;
        ToEmail = toEmail;
        Subject = subject;
        Body = body;
        Attachments = attachments;
    }

    public MailVM(string toName, string toEmail, string subject, string body, ICollection<string> carbonCopies, ICollection<MailAttachmentVM> attachments)
    {
        ToName = toName;
        ToEmail = toEmail;
        Subject = subject;
        Body = body;
        CarbonCopies = carbonCopies;
        Attachments = attachments;
    }

    public string ToEmail { get; set; }
    public string ToName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public ICollection<string> CarbonCopies { get; set; }
    public ICollection<MailAttachmentVM> Attachments { get; set; }
}
