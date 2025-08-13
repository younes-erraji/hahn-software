using HahnSoftware.Domain.Events.Posts;

using MediatR;

using Microsoft.Extensions.Logging;

namespace HahnSoftware.Application.Posts.EventHandlers;

public class PostCreationEventHandler : INotificationHandler<PostCreationEvent>
{
    private readonly ILogger<PostCreationEventHandler> _logger;

    public PostCreationEventHandler(ILogger<PostCreationEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PostCreationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Post created: {PostId}", notification.PostId);
        return Task.CompletedTask;
    }
}
