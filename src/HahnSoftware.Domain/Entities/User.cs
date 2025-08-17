using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Domain.Events.Users;

namespace HahnSoftware.Domain.Entities;

public sealed class User : Entity
{
    public string FullName => $"{FirstName} {LastName}";
    public string Key { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Mail { get; private set; }
    public string? Avatar { get; private set; }
    public string Password { get; private set; }
    public bool MailVerification { get; private set; } = false;
    public string? MailVerificationToken { get; private set; }
    public DateTimeOffset? MailVerificationTokenExpiry { get; private set; }
    public string? PasswordResetToken { get; private set; }
    public DateTimeOffset? PasswordResetTokenExpiry { get; private set; }
    public DateTimeOffset? LoginDate { get; private set; }

    public ICollection<Post> Posts { get; private set; } = new HashSet<Post>();
    public ICollection<Comment> Comments { get; private set; } = new HashSet<Comment>();
    public ICollection<PostBookmark> Bookmarks { get; private set; } = new HashSet<PostBookmark>();
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = new HashSet<RefreshToken>();

    public User() { }

    public User(string key, string firstName, string lastName, string mail, string password, string verificationToken, DateTimeOffset tokenExpiry)
    {
        Key = key;
        FirstName = firstName;
        LastName = lastName;
        Mail = mail;
        Password = password;
        MailVerification = false;
        MailVerificationToken = verificationToken;
        MailVerificationTokenExpiry = tokenExpiry;
        AddDomainEvent(new UserRegistrationEvent(Id, Mail, MailVerificationToken));
    }

    public void ConfirmMail()
    {
        if (MailVerification)
            return;

        MailVerification = true;
        AddDomainEvent(new UserVerificationEvent(Id, Mail));
    }

    public void ChangePassword(string password)
    {
        Password = password;
        AddDomainEvent(new ChangePasswordEvent(Id, Mail));
    }
    
    public void ForgotPassword(string resetToken, DateTimeOffset tokenExpiry)
    {
        PasswordResetToken = resetToken;
        PasswordResetTokenExpiry = tokenExpiry;
        AddDomainEvent(new ForgotPasswordEvent(Id, Mail, PasswordResetToken));
    }

    public void Login()
    {
        LoginDate = DateTimeOffset.Now;
        AddDomainEvent(new UserLoginEvent(Id, Mail));
    }

    public void VerifyMail()
    {
        MailVerification = true;
        MailVerificationToken = null;
        MailVerificationTokenExpiry = null;
        AddDomainEvent(new MailVerificationEvent(Id, Mail));
    }
    
    public void ResendMailVerification(string verificationToken, DateTimeOffset tokenExpiry)
    {
        MailVerificationToken = verificationToken;
        MailVerificationTokenExpiry = tokenExpiry;
        AddDomainEvent(new ResendMailVerificationEvent(Id, Mail, MailVerificationToken));
    }

    public void ResetPassword(string password)
    {
        Password = password;
        PasswordResetToken = null;
        PasswordResetTokenExpiry = null;
        AddDomainEvent(new ResetPasswordEvent(Id, Mail));
    }
}