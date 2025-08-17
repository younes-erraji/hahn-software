namespace HahnSoftware.Domain.Events.Users;

public class ForgotPasswordEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public string Mail { get; set; }
    public string PasswordResetToken { get; set; }

    public ForgotPasswordEvent(Guid userId, string mail, string passwordResetToken)
    {
        UserId = userId;
        Mail = mail;
        PasswordResetToken = passwordResetToken;
    }
}
