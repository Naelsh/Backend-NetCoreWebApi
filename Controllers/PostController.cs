﻿namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Posts;
using WebApi.Services;

[Authorize]
[ApiController]
[Route("controller/post")]
public class PostController : ControllerBase
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
    [HttpPost]
    public IActionResult Post(PostRequest model)
    {
        _postService.Post(model);
        return Ok(new { message = "Post created successfully" });
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _postService.Update(id, model);
        return Ok(new { message = "Post updated successfully" });
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _postService.Delete(id);
        return Ok(new { message = "Post deleted successfully" });
    }
}