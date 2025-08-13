using HahnSoftware.Domain.Entities.Primitives;

namespace HahnSoftware.Domain.Entities;

public sealed class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mail { get; set; }
    public ICollection<Post> Posts { get; private set; } = new HashSet<Post>();
    public ICollection<Comment> Comments { get; private set; } = new HashSet<Comment>();
    public ICollection<PostBookmark> Bookmarks { get; private set; } = new HashSet<PostBookmark>();

    public User(string firstName, string lastName, string mail)
    {
        FirstName = firstName;
        LastName = lastName;
        Mail = mail;
    }

    public string FullName => $"{FirstName} {LastName}";
}