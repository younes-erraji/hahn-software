using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using HahnSoftware.Application.Posts.Queries;
using HahnSoftware.Domain.RESTful;
using HahnSoftware.Application.Posts.DTO;
using HahnSoftware.Application.Posts.Commands.CreatePost;
using HahnSoftware.Application.Posts.Commands.UpdatePost;

namespace HahnSoftware.API.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPost(Guid id)
    {
        GetPostQuery query = new GetPostQuery(id);
        PostDetailDTO? result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookmarkPostCommand command)
    {
        Response response = await _mediator.Send(command);
        // return CreatedAtAction(nameof(GetPost), new { id = result }, null);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePostCommand command)
    {
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}
