using HahnSoftware.Application.Comments.Commands.DeleteCommentReaction;
using HahnSoftware.Application.Comments.Commands.LikeComment;
using HahnSoftware.Application.RESTful;
using HahnSoftware.Domain.Enums;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HahnSoftware.API.Controllers;

[Authorize]
[ApiController]
[Route("api/comments")]
public class CommantReactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommantReactionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("like")]
    public async Task<IActionResult> Like([FromQuery] Guid commentId)
    {
        LikeDislikeCommentCommand command = new LikeDislikeCommentCommand(ReactionType.Like, commentId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("dislike")]
    public async Task<IActionResult> Dislike([FromQuery] Guid commentId)
    {
        LikeDislikeCommentCommand command = new LikeDislikeCommentCommand(ReactionType.Dislike, commentId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid commentId)
    {
        DeleteCommentReactionCommand command = new DeleteCommentReactionCommand(commentId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}
