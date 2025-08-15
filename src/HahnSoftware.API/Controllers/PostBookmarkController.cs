using HahnSoftware.Application.Posts.Commands.CreatePost;
using HahnSoftware.Application.RESTful;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HahnSoftware.API.Controllers;

[Authorize]
[ApiController]
[Route("api/posts")]
public class PostBookmarkController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostBookmarkController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("bookmark")]
    public async Task<IActionResult> Bookmark([FromQuery] Guid postId)
    {
        BookmarkPostCommand command = new BookmarkPostCommand(postId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpGet("unbookmark")]
    public async Task<IActionResult> Unbookmark([FromQuery] Guid postId)
    {
        UnbookmarkPostCommand command = new UnbookmarkPostCommand(postId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}
