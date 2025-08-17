using HahnSoftware.Application.Posts.Commands.CreatePost;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Entities;
using HahnSoftware.Domain.Interfaces.Repositories;
using HahnSoftware.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HahnSoftware.Application.UnitTest.Posts.Commands;

public class BookmarkPostCommandHandlerTests
{
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IPostBookmarkRepository> _postBookmarkRepositoryMock;
    private readonly BookmarkPostCommandHandler _handler;

    public BookmarkPostCommandHandlerTests()
    {
        _postRepositoryMock = new Mock<IPostRepository>();
        _userServiceMock = new Mock<IUserService>();
        _postBookmarkRepositoryMock = new Mock<IPostBookmarkRepository>();

        _handler = new BookmarkPostCommandHandler(
            _postRepositoryMock.Object,
            _userServiceMock.Object,
            _postBookmarkRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_PostDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        Guid postId = Guid.CreateVersion7();
        BookmarkPostCommand command = new BookmarkPostCommand(postId);
        var userId = Guid.NewGuid();

        _userServiceMock.Setup(x => x.GetUserIdentifier()).Returns(userId);
        _postRepositoryMock.Setup(x => x.Exists(command.Id, CancellationToken.None)).ReturnsAsync(false);

        // Act
        Response result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Status);
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        _postBookmarkRepositoryMock.Verify(x => x.Create(It.IsAny<PostBookmark>(), It.IsAny<CancellationToken>()), Times.Never);
        _postBookmarkRepositoryMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Never);
    }

    /*
    [Fact]
    public async Task Handle_PostAlreadyBookmarked_ReturnsBadRequest()
    {
        // Arrange
        Guid postId = Guid.CreateVersion7();
        BookmarkPostCommand command = new BookmarkPostCommand(postId);
        Guid userId = Guid.NewGuid();

        _userServiceMock.Setup(x => x.GetUserIdentifier()).Returns(userId);
        _postRepositoryMock.Setup(x => x.Exists(command.Id, CancellationToken.None)).ReturnsAsync(true);

        // Act
        Response result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Status);
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        Assert.Equal("This post is already bookmarked!", result.Message);
        _postBookmarkRepositoryMock.Verify(x => x.Create(It.IsAny<PostBookmark>(), It.IsAny<CancellationToken>()), Times.Never);
        _postBookmarkRepositoryMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Never);
    }
    */

    [Fact]
    public async Task Handle_ValidRequest_CreatesBookmarkAndReturnsSuccess()
    {
        // Arrange
        Guid postId = Guid.CreateVersion7();
        BookmarkPostCommand command = new BookmarkPostCommand(postId);
        Guid userId = Guid.CreateVersion7();

        _userServiceMock.Setup(x => x.GetUserIdentifier()).Returns(userId);
        _postRepositoryMock.Setup(x => x.Exists(command.Id, CancellationToken.None)).ReturnsAsync(true);

        // Act
        Response result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Status);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        _postBookmarkRepositoryMock.Verify(x => x.Create(
            It.Is<PostBookmark>(pb => pb.PostId == command.Id && pb.UserId == userId),
            It.IsAny<CancellationToken>()), Times.Once);
        _postBookmarkRepositoryMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrowsException_ShouldThrow()
    {
        // Arrange
        Guid postId = Guid.CreateVersion7();
        BookmarkPostCommand command = new BookmarkPostCommand(postId);
        Guid userId = Guid.CreateVersion7();

        _userServiceMock.Setup(x => x.GetUserIdentifier()).Returns(userId);
        _postRepositoryMock.Setup(x => x.Exists(command.Id, CancellationToken.None)).ReturnsAsync(true);

        _postBookmarkRepositoryMock
            .Setup(x => x.Create(It.IsAny<PostBookmark>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}
