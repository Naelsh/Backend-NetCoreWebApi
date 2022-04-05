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
[Route("[controller]")]
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

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var eventItem = _eventService.GetById(id);
        return Ok(eventItem);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var eventIItems = _eventService.GetAll();
        return Ok(eventIItems);
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
        return Ok(new {message = "User deleted successfully"});
    }
}