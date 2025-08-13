namespace HahnSoftware.Domain.Events.Users;

public sealed class UserVerificationEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Mail { get; set; }

    public UserVerificationEvent(Guid userId, string mail)
    {
        UserId = userId;
        Mail = mail;
    }
}
