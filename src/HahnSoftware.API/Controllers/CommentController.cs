using HahnSoftware.API.DTO.Comments;
using HahnSoftware.Application.Comments.DTO;
using HahnSoftware.Application.Comments.Queries.GetPostComments;
using HahnSoftware.Application.Comments.Commands.CreateComment;
using HahnSoftware.Application.Comments.Commands.UpdatePost;
using HahnSoftware.Application.Comments.Commands.DeletePost;
using HahnSoftware.Application.RESTful;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HahnSoftware.API.Controllers;

[ApiController]
[Route("api/comments/comments")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPostComments([FromQuery] Guid postId)
    {
        GetPostCommentsQuery query = new GetPostCommentsQuery(postId);
        IEnumerable<CommentDetailDTO> result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CommentInputDTO commentDto)
    {
        CreateCommentCommand command = new CreateCommentCommand(commentDto.PostId, commentDto.Content);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CommentUpdateDTO commentDto)
    {
        UpdateCommentCommand command = new UpdateCommentCommand(commentDto.CommentId, commentDto.Content);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid commentId)
    {
        DeleteCommentCommand command = new DeleteCommentCommand(commentId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}
