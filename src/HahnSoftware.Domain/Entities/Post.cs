using HahnSoftware.Domain.Enums;
using HahnSoftware.Domain.Events.Posts;
using HahnSoftware.Domain.Entities.Primitives;

using System.Text.RegularExpressions;

namespace HahnSoftware.Domain.Entities;

public sealed class Post : Entity
{
    public string Slug { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
    public Guid UserId { get; private set; }
    public bool Archive { get; private set; } = false;
    public DateTimeOffset? ArchiveDate { get; private set; }
    public DateTimeOffset? UpdateDate { get; private set; }
    public List<string> Tags { get; private set; } = new();

    public User User { get; private set; }


    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    private readonly List<PostBookmark> _bookmarks = new();
    public IReadOnlyCollection<PostBookmark> Bookmarks => _bookmarks.AsReadOnly();
    
    private readonly List<PostReaction> _reactions = new();
    public IReadOnlyCollection<PostReaction> Reactions => _reactions.AsReadOnly();
    
    private readonly List<PostAttachment> _attachments = new();
    public IReadOnlyCollection<PostAttachment> Attachments => _attachments.AsReadOnly();

    /*
    public ICollection<Comment> Comments { get; private set; } = new HashSet<Comment>();
    public ICollection<PostReaction> Reactions { get; private set; } = new HashSet<PostReaction>();
    public ICollection<PostBookmark> Bookmarks { get; private set; } = new HashSet<PostBookmark>();
    public ICollection<PostAttachment> Attachments { get; private set; } = new HashSet<PostAttachment>();
    */

    private Post() { }

    public Post(string title, string body, List<string> tags, Guid userId)
    {
        Slug = GenerateSlug(title);
        Title = title;
        Body = body;
        Tags = tags ?? new List<string>();
        UserId = userId;

        AddDomainEvent(new PostCreationEvent(Id, UserId, title));
    }

    public void Update(string title, string body, List<string> tags)
    {
        Slug = GenerateSlug(title);
        Title = title;
        Body = body;
        Tags = tags ?? new List<string>();
        UpdateDate = DateTimeOffset.Now;

        AddDomainEvent(new PostUpdateEvent(Id, title));
    }
    
    public void Delete()
    {
        DeletionDate = DateTimeOffset.Now;

        AddDomainEvent(new PostDeleteEvent(Id));
    }

    public void ArchivePost()
    {
        Archive = true;
        ArchiveDate = DateTimeOffset.Now;
        AddDomainEvent(new PostArchiveEvent(Id));
    }
    
    public void Bookmark(Guid userId)
    {
        if (_bookmarks.Any(x => x.UserId == userId))
            throw new InvalidOperationException("Post is already bookmarked by this user");

        PostBookmark bookmark = new PostBookmark(Id, userId);
        _bookmarks.Add(bookmark);

        AddDomainEvent(new PostBookmarkEvent(Id, bookmark.UserId));
    }

    public void Unbookmark(Guid userId)
    {
        PostBookmark? bookmark = _bookmarks.FirstOrDefault(b => b.UserId == userId);
        if (bookmark is not null)
        {
            _bookmarks.Remove(bookmark);
            AddDomainEvent(new PostUnbookmarkEvent(Id, userId));
        }
    }

    public void React(Guid userId, ReactionType type)
    {
        if (_reactions.Any(x => x.UserId == userId))
            throw new InvalidOperationException("Post is already reacted by this user");

        PostReaction reaction = new PostReaction(Id, userId, type);
        _reactions.Add(reaction);
        AddDomainEvent(new PostReactionCreateEvent(Id, reaction.UserId, reaction.Type));
    }
    
    public void Unreact(Guid userId)
    {
        PostReaction? reaction = _reactions.FirstOrDefault(x => x.UserId == userId);
        if (reaction is not null)
        {
            _reactions.Remove(reaction);
            AddDomainEvent(new PostReactionRemoveEvent(Id, userId));
        }
    }

    private static string GenerateSlug(string title)
    {
        string slug = title
            .ToLowerInvariant()
            .Replace(" ", "-");

        slug = Regex.Replace(slug, @"[^a-z0-9\-]", "-");
        slug = Regex.Replace(slug, @"-+", "-");
        slug = slug.Trim('-');

        return slug;
    }
}
