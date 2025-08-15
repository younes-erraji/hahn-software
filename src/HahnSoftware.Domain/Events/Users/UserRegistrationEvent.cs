namespace HahnSoftware.Domain.Events.Users;

public sealed class UserRegistrationEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Mail { get; set; }
    public string VerificationToken { get; set; }

    public UserRegistrationEvent(Guid userId, string mail, string verificationToken)
    {
        UserId = userId;
        Mail = mail;
        VerificationToken = verificationToken;
    }
}
