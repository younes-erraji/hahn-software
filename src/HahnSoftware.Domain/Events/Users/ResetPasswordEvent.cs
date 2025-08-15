namespace HahnSoftware.Domain.Events.Users;

public class ResetPasswordEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Mail { get; set; }

    public ResetPasswordEvent(Guid userId, string mail)
    {
        UserId = userId;
        Mail = mail;
    }
}
