using HahnSoftware.Application.Posts.Commands.DeletePostReaction;
using HahnSoftware.Application.Posts.Commands.LikePost;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HahnSoftware.API.Controllers;

[Authorize]
[ApiController]
[Route("api/posts/reaction")]
public class PostReactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostReactionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("like")]
    public async Task<IActionResult> Like([FromQuery] Guid postId)
    {
        LikeDislikePostCommand command = new LikeDislikePostCommand(ReactionType.Like, postId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("dislike")]
    public async Task<IActionResult> Dislike([FromQuery] Guid postId)
    {
        LikeDislikePostCommand command = new LikeDislikePostCommand(ReactionType.Dislike, postId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid postId)
    {
        DeletePostReactionCommand command = new DeletePostReactionCommand(postId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}
