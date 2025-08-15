namespace HahnSoftware.Domain.Events.Users;

public class ForgotPasswordEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Mail { get; set; }

    public ForgotPasswordEvent(Guid userId, string mail)
    {
        UserId = userId;
        Mail = mail;
    }
}
