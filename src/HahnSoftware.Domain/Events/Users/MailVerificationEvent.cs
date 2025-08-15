namespace HahnSoftware.Domain.Events.Users;

public class MailVerificationEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Mail { get; set; }

    public MailVerificationEvent(Guid userId, string mail)
    {
        UserId = userId;
        Mail = mail;
    }
}
