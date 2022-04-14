namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Posts;
using WebApi.Services;

public class PostController : BaseController
{
    private IPostService _postService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public PostController(
        IPostService postService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _postService = postService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var postItem = _postService.GetById(id);
        return Ok(postItem);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetAll()
    {
        var postItems = _postService.GetAll();
        return Ok(postItems);
    }

    [AllowAnonymous]
    [HttpGet("details/{authorId}")]
    public IActionResult GetDetailedById(int authorId)
    {
        var postItems = _postService.GetDetailedPostsForUserById(authorId);
        return Ok(postItems);
    }

    [HttpPost]
    public IActionResult Post(PostRequest model)
    {
        _postService.Post(model, GetAuthenticatedUser());
        return Ok(new { message = "Post created successfully" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _postService.Update(id, model);
        return Ok(new { message = "Post updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _postService.Delete(id);
        return Ok(new { message = "Post deleted successfully" });
    }
}