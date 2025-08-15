using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Domain.Events.Comments;
using HahnSoftware.Domain.Events.Posts;
using HahnSoftware.Domain.Enums;

namespace HahnSoftware.Domain.Entities;

public sealed class Comment : Entity
{
    public Comment(string content, Guid postId, Guid userId)
    {
        Content = content;
        PostId = postId;
        UserId = userId;

        AddDomainEvent(new CommentCreationEvent(PostId, Id, Content, UserId));
    }
    
    public Comment(string content, Guid postId, Guid userId, Guid commentId)
    {
        Content = content;
        PostId = postId;
        UserId = userId;
        ThreadCommentId = commentId;
        Repliable = false;

        AddDomainEvent(new CommentReplyEvent(PostId, Id, Content, UserId, ThreadCommentId.Value));
    }

    public string Content { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid PostId { get; private set; }
    public Post Post { get; private set; }

    public bool Repliable { get; private set; } = true;
    public Guid? ThreadCommentId { get; private set; }
    public DateTimeOffset? UpdateDate { get; private set; }

    public Comment? ThreadComment { get; private set; }

    private readonly List<CommentReaction> _reactions = new();
    public IReadOnlyCollection<CommentReaction> Reactions => _reactions.AsReadOnly();
    
    private readonly List<Comment> _replies = new();
    public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();

    public void Update(string content)
    {
        Content = content;
        UpdateDate = DateTimeOffset.Now;
        AddDomainEvent(new CommentUpdateEvent(Id, content));
    }

    public void Delete()
    {
        DeletionDate = DateTimeOffset.Now;
        AddDomainEvent(new CommentDeleteEvent(Id));
    }

    public void Reply(string content, Guid userId)
    {
        Comment reply = new Comment(content, PostId, userId, Id);
        _replies.Add(reply);
        AddDomainEvent(new CommentReplyEvent(PostId, reply.Id, content, userId, Id));
    }

    public void LikeDislike(Guid userId, ReactionType type)
    {
        if (_reactions.Any(x => x.UserId == userId))
            throw new InvalidOperationException("Comment is already reacted by this user");

        CommentReaction reaction = new CommentReaction(Id, userId, type);
        _reactions.Add(reaction);
        AddDomainEvent(new CommentReactionCreateEvent(Id, reaction.UserId, reaction.Type));
    }

    public void DeleteReaction(Guid userId)
    {
        CommentReaction? reaction = _reactions.FirstOrDefault(x => x.UserId == userId);
        if (reaction is not null)
        {
            _reactions.Remove(reaction);
            AddDomainEvent(new CommentReactionDeleteEvent(Id, userId));
        }
    }
}
