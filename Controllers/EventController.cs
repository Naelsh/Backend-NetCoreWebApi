namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Events;
using WebApi.Services;

[Authorize]
[ApiController]
[Route("controller/event")]
public class EventController : ControllerBase
{
    private IEventService _eventService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public EventController(
        IEventService eventService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _eventService = eventService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var eventItem = _eventService.GetById(id);
        return Ok(eventItem);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetAll()
    {
        var eventItems = _eventService.GetAll();
        return Ok(eventItems);
    }

    [HttpGet("details/{id}")]
    public IActionResult GetDetailById(int id)
    {
        var eventItems = _eventService.GetDetailById(id);
        return Ok(eventItems);
    }

    [HttpPost]
    public IActionResult Post(PostRequest model)
    {
        _eventService.Post(model);
        return Ok(new {message = "Event created successfully"});
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _eventService.Update(id, model);
        return Ok(new { message = "Event updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _eventService.Delete(id);
        return Ok(new {message = "Event deleted successfully"});
    }
}