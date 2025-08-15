namespace HahnSoftware.Domain.Events.Users;

public class ResendMailVerificationEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Mail { get; set; }
    public string VerificationToken { get; set; }

    public ResendMailVerificationEvent(Guid userId, string mail, string verificationToken)
    {
        UserId = userId;
        Mail = mail;
        VerificationToken = verificationToken;
    }
}
