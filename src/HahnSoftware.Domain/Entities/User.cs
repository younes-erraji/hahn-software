using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Domain.Events.Users;

namespace HahnSoftware.Domain.Entities;

public sealed class User : Entity
{
    public string FullName => $"{FirstName} {LastName}";
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mail { get; set; }
    public string? Avatar { get; set; }
    public string Password { get; set; }
    public bool MailVerification { get; set; } = false;
    public string? MailVerificationToken { get; set; }
    public DateTimeOffset? MailVerificationTokenExpiry { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTimeOffset? PasswordResetTokenExpiry { get; set; }
    public DateTimeOffset? LoginDate { get; set; }

    public ICollection<Post> Posts { get; private set; } = new HashSet<Post>();
    public ICollection<Comment> Comments { get; private set; } = new HashSet<Comment>();
    public ICollection<PostBookmark> Bookmarks { get; private set; } = new HashSet<PostBookmark>();
    public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    private User() { }

    public User(string firstName, string lastName, string mail, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Mail = mail;
        Password = password;

        AddDomainEvent(new UserRegistrationEvent(Id, Mail));
    }

    public void ConfirmMail()
    {
        if (MailVerification)
            return;

        MailVerification = true;
        AddDomainEvent(new UserVerificationEvent(Id, Mail));
    }

    public void Login()
    {
        LoginDate = DateTimeOffset.Now;
        AddDomainEvent(new UserLoginEvent(Id, Mail));
    }
}