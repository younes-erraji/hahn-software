using HahnSoftware.Application.Posts.Commands.CreatePost;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;

using Moq;

namespace HahnSoftware.Application.UnitTest.Posts.Commands;

public class CreatePostCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreatePostAndReturnSuccess()
    {
        // Arrange
        Mock<IPostRepository> mockPostRepo = new Mock<IPostRepository>();
        Mock<IUserService> mockUserService = new Mock<IUserService>();
        Guid userId = Guid.CreateVersion7();
        mockUserService.Setup(x => x.GetUserIdentifier()).Returns(userId);
        CreatePostCommandHandler handler = new CreatePostCommandHandler(mockPostRepo.Object, mockUserService.Object);
        CreatePostCommand command = new CreatePostCommand("Test Title", "Test Body", new List<string> { "tag1", "tag2" });

        // Act
        Response? result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockPostRepo.Verify(x => x.Create(It.Is<Post>(p => p.Title == "Test Title" && p.Body == "Test Body" && p.Tags.SequenceEqual(command.Tags) && p.UserId == userId), CancellationToken.None), Times.Once);
        mockPostRepo.Verify(x => x.SaveChanges(CancellationToken.None), Times.Once);
        Assert.True(result.Status);
        Assert.Equal(Response.Success().StatusCode, result.StatusCode);
    }
}
