namespace HahnSoftware.Application.UnitTest.Posts.Commands;

public class UnbookmarkPostCommandHandlerTests
{
    /*
    [Fact]
    public async Task Handle_ShouldUnbookmarkPostAndReturnSuccess()
    {
        // Arrange
        var mockRepo = new Mock<IPostRepository>();
        var postId = Guid.NewGuid();
        var post = new Post("Title", "Body", new List<string> { "tag" }, Guid.NewGuid()) { Id = postId };
        mockRepo.Setup(x => x.GetById(postId)).ReturnsAsync(post);
        var handler = new UnbookmarkPostCommandHandler(mockRepo.Object);
        var command = new UnbookmarkPostCommand(postId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepo.Verify(x => x.GetById(postId), Times.Once);
        mockRepo.Verify(x => x.Unbookmark(It.Is<Post>(p => p.Id == postId)), Times.Once);
        mockRepo.Verify(x => x.SaveChanges(), Times.Once);
        Assert.True(result.Status);
        Assert.Equal(Response.Success().StatusCode, result.StatusCode);
    }
    */
}
