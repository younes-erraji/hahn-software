using Microsoft.AspNetCore.Mvc;

using MediatR;

using HahnSoftware.Application.RESTful;
using HahnSoftware.Application.Posts.DTO;
using HahnSoftware.Application.Posts.Commands.CreatePost;
using HahnSoftware.Application.Posts.Commands.UpdatePost;
using HahnSoftware.Application.Posts.Commands.DeletePost;
using HahnSoftware.Domain.Pagination;
using HahnSoftware.Domain.Entities;
using HahnSoftware.API.DTO.Posts;
using HahnSoftware.Application.Posts.Queries.GetPost;
using HahnSoftware.Application.Posts.Queries.GetPosts;
using Microsoft.AspNetCore.Authorization;

namespace HahnSoftware.API.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts([FromQuery] PaginationParam pagination, [FromBody] PostFilterDTO filter)
    {
        GetPostsQuery query = new GetPostsQuery(filter.Search, pagination);
        PageableResponse<Post, PostListDTO> result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetPost([FromRoute] string slug)
    {
        GetPostQuery query = new GetPostQuery(slug);
        PostDetailDTO? result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostInputDTO postDto)
    {
        CreatePostCommand command = new CreatePostCommand(postDto.Title, postDto.Body, postDto.Tags);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] PostUpdateDTO postDto)
    {
        UpdatePostCommand command = new UpdatePostCommand(postDto.Id, postDto.Title, postDto.Body, postDto.Tags);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid postId)
    {
        DeletePostCommand command = new DeletePostCommand(postId);
        Response response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}
