namespace HahnSoftware.Domain.Events.Users;

public class ChangePasswordEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Mail { get; set; }

    public ChangePasswordEvent(Guid userId, string mail)
    {
        UserId = userId;
        Mail = mail;
    }
}
