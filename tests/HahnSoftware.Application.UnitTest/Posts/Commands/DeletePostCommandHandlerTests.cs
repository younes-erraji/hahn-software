using HahnSoftware.Application.Posts.Commands.DeletePost;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;

using Moq;

namespace HahnSoftware.Application.UnitTest.Posts.Commands;

public class DeletePostCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDeletePostAndReturnSuccess()
    {
        // Arrange
        Mock<IPostRepository> mockPostRepo = new Mock<IPostRepository>();
        Guid postId = Guid.CreateVersion7();
        Post post = new Post(postId, "Title", "Body", new List<string> { "tag" }, Guid.CreateVersion7());
        mockPostRepo.Setup(x => x.Get(postId, CancellationToken.None)).ReturnsAsync(post);
        // post.Delete();
        DeletePostCommandHandler handler = new DeletePostCommandHandler(mockPostRepo.Object);
        DeletePostCommand command = new DeletePostCommand(postId);

        // Act
        Response result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockPostRepo.Verify(x => x.Get(postId, CancellationToken.None), Times.Once);
        // mockPostRepo.Verify(x => x.Update(It.Is<Post>(p => p.Id == postId), CancellationToken.None), Times.Once);
        mockPostRepo.Verify(x => x.SaveChanges(CancellationToken.None), Times.Once);
        Assert.True(result.Status);
        Assert.Equal(Response.Success().StatusCode, result.StatusCode);
    }
}
