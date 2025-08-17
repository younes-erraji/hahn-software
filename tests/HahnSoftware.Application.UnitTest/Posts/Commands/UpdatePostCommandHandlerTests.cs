using HahnSoftware.Application.Posts.Commands.UpdatePost;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;

using Moq;

namespace HahnSoftware.Application.UnitTest.Posts.Commands
{
    public class UpdatePostCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdatePostAndReturnSuccess()
        {
            // Arrange
            Mock<IPostRepository> mockRepo = new Mock<IPostRepository>();
            Guid postId = Guid.CreateVersion7();
            Post post = new Post(postId, "Old Title", "Old Body", new List<string> { "old" }, Guid.NewGuid());
            mockRepo.Setup(x => x.Get(postId, CancellationToken.None)).ReturnsAsync(post);
            UpdatePostCommandHandler handler = new UpdatePostCommandHandler(mockRepo.Object);
            UpdatePostCommand command = new UpdatePostCommand(postId, "New Title", "New Body", new List<string> { "new" });

            // Act
            Response result = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepo.Verify(x => x.Get(postId, CancellationToken.None), Times.Once);
            mockRepo.Verify(x => x.Update(It.Is<Post>(p =>
                p.Id == postId &&
                p.Title == "New Title" &&
                p.Body == "New Body" &&
                p.Tags.Count == command.Tags.Count &&
                !p.Tags.Except(command.Tags).Any() &&
                !command.Tags.Except(p.Tags).Any()
            ), CancellationToken.None), Times.Once);
            mockRepo.Verify(x => x.SaveChanges(CancellationToken.None), Times.Once);
            Assert.True(result.Status);
            Assert.Equal(Response.Success().StatusCode, result.StatusCode);
        }
    }
}
