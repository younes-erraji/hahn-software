using HahnSoftware.Application.Posts.Commands.CreatePost;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using Moq;

namespace HahnSoftware.Application.UnitTest.Posts.Commands;

public class BookmarkPostCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldBookmarkPostAndReturnSuccess()
    {
        // Arrange
        Mock<IPostBookmarkRepository> mockPostBookmarkRepo = new Mock<IPostBookmarkRepository>();
        Mock<IPostRepository> mockPostRepo = new Mock<IPostRepository>();
        Mock<IUserService> mockUserService = new Mock<IUserService>();
        Guid userId = Guid.CreateVersion7();
        mockUserService.Setup(x => x.GetUserIdentifier()).Returns(userId);
        Guid postId = Guid.NewGuid();
        Post post = new Post(postId, "Title", "Body", new List<string> { "tag" }, Guid.NewGuid());
        mockPostRepo.Setup(x => x.Get(postId, CancellationToken.None)).ReturnsAsync(post);
        BookmarkPostCommandHandler handler = new BookmarkPostCommandHandler(mockPostRepo.Object, mockUserService.Object, mockPostBookmarkRepo.Object);
        BookmarkPostCommand command = new BookmarkPostCommand(postId);

        // Act
        Response result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockPostRepo.Verify(x => x.Get(postId, CancellationToken.None), Times.Once);
        mockPostBookmarkRepo.Verify(x => x.Create(It.Is<PostBookmark>(x => x.PostId == postId), CancellationToken.None), Times.Once);
        mockPostBookmarkRepo.Verify(x => x.SaveChanges(CancellationToken.None), Times.Once);
        Assert.True(result.Status);
        Assert.Equal(Response.Success().StatusCode, result.StatusCode);
    }
}
