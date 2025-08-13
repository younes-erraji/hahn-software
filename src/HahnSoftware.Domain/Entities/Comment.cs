using HahnSoftware.Domain.Entities.Primitives;
using HahnSoftware.Domain.Enums;
using HahnSoftware.Domain.Events.Comments;
using HahnSoftware.Domain.Events.Posts;

namespace HahnSoftware.Domain.Entities;

public sealed class Comment : Entity
{
    public Comment(string content, Guid postId, Guid userId)
    {
        Content = content;
        PostId = postId;
        UserId = userId;
    }
    
    public Comment(string content, Guid postId, Guid userId, Guid commentId)
    {
        Content = content;
        PostId = postId;
        UserId = userId;
        ThreadCommentId = commentId;
        Repliable = false;
    }

    public string Content { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid PostId { get; private set; }
    public Post Post { get; private set; }

    public bool Repliable { get; private set; } = true;
    public Guid? ThreadCommentId { get; private set; }

    public Comment? ThreadComment { get; private set; }

    private readonly List<CommentReaction> _reactions = new();
    public IReadOnlyCollection<CommentReaction> Reactions => _reactions.AsReadOnly();
    
    private readonly List<Comment> _replies = new();
    public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();

    public void Reply(string content, Guid userId)
    {
        Comment reply = new Comment(content, PostId, userId, Id);
        _replies.Add(reply);
        AddDomainEvent(new CommentReplyEvent(PostId, reply.Id, content, userId, Id));
    }

    public void React(Guid userId, ReactionType type)
    {
        if (_reactions.Any(x => x.UserId == userId))
            throw new InvalidOperationException("Post is already reacted by this user");

        CommentReaction reaction = new CommentReaction(Id, userId, type);
        _reactions.Add(reaction);
        AddDomainEvent(new PostReactionCreateEvent(Id, reaction.UserId, reaction.Type));
    }

    public void Unreact(Guid userId)
    {
        CommentReaction? reaction = _reactions.FirstOrDefault(x => x.UserId == userId);
        if (reaction is not null)
        {
            _reactions.Remove(reaction);
            AddDomainEvent(new PostReactionRemoveEvent(Id, userId));
        }
    }

}
