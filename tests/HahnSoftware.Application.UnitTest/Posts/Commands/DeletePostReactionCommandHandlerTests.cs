using HahnSoftware.Application.Posts.Commands.DeletePostReaction;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;

using Moq;

namespace HahnSoftware.Application.UnitTest.Posts.Commands;

public class DeletePostReactionCommandHandlerTests
{
    /*
    [Fact]
    public async Task Handle_ShouldDeleteReactionAndReturnSuccess()
    {
        // Arrange
        var mockRepo = new Mock<IPostRepository>();
        var postId = Guid.NewGuid();
        var post = new Post(postId, "Title", "Body", new List<string> { "tag" }, Guid.NewGuid());
        mockRepo.Setup(x => x.Get(postId)).ReturnsAsync(post);
        var handler = new DeletePostReactionCommandHandler(mockRepo.Object);
        var command = new DeletePostReactionCommand(postId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepo.Verify(x => x.Get(postId), Times.Once);
        mockRepo.Verify(x => x.DeleteReaction(It.Is<Post>(p => p.Id == postId)), Times.Once);
        mockRepo.Verify(x => x.SaveChanges(), Times.Once);
        Assert.True(result.Status);
        Assert.Equal(Response.Success().StatusCode, result.StatusCode);
    }
    */
}
