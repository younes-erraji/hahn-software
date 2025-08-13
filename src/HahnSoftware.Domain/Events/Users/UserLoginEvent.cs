namespace HahnSoftware.Domain.Events.Users;

public class UserLoginEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Mail { get; set; }

    public UserLoginEvent(Guid userId, string mail)
    {
        UserId = userId;
        Mail = mail;
    }
}
