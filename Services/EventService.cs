namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Events;

public interface IEventService
{
    IEnumerable<EventItem> GetAll();
    EventItem GetById(int id);
    void Post(PostRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);
}

public class EventService : IEventService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public EventService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<EventItem> GetAll()
    {
        return _context.EventItems;
    }

    public EventItem GetById(int id)
    {
        return GetEvent(id);
    }

    public void Post(PostRequest model)
    {
        // validate
        if (DateTime.Compare(model.StartDate, model.EndDate) < 1)
            throw new AppException("Event '" + model.Title + "' start date after end date");

        // map model to new event object
        var eventItem = _mapper.Map<EventItem>(model);

        // save event
        _context.EventItems.Add(eventItem);
        _context.SaveChanges();
    }

    public void Update(int id, UpdateRequest model)
    {
        var eventItem = GetEvent(id);

        // validate
        if (DateTime.Compare(model.StartDate, model.EndDate) < 1)
            throw new AppException("Event '" + model.Title + "' start date after end date");

        // copy model to event and save
        _mapper.Map(model, eventItem);
        _context.EventItems.Update(eventItem);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var eventItem = GetEvent(id);
        _context.EventItems.Remove(eventItem);
        _context.SaveChanges();
    }

    // helper methods

    private EventItem GetEvent(int id)
    {
        var eventItem = _context.EventItems.Find(id);
        if (eventItem == null) throw new KeyNotFoundException("Event not found");
        return eventItem;
    }
}