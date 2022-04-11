namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Events;
using WebApi.Views;

public interface IEventService
{
    IEnumerable<EventItem> GetAll();
    EventItem GetById(int id);
    void Post(PostRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);
    EventDetailView GetDetailById(int id);
    IEnumerable<EventDetailView> GetAllDetail();
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
        if (DateTime.Compare(model.StartDate, model.EndDate) > 0)
            throw new AppException("Event '" + model.Title + "' start date after end date");

        var user = _context.Users.FirstOrDefault(x => x.Id == model.CreatorId);

        // map model to new event object
        var eventItem = new EventItem
        {
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
        };
        _context.EventItems.Add(eventItem);
        
        var eventUser = new EventUsers
        {
            User = user,
            EventItem = eventItem,
            IsHost = true
        };
        _context.EventUsers.Add(eventUser);

        // save event
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

    public EventDetailView GetDetailById(int id)
    {
        return GetDetail(id);
    }

    public IEnumerable<EventDetailView> GetAllDetail()
    {
        var eventItems = _context.EventItems;
        var eventDetailViews = new List<EventDetailView>();
        foreach (var item in eventItems)
        {
            var participants = from partici in _context.Users
                               join eventUsers in _context.EventUsers on partici.Id equals eventUsers.UserId
                               where eventUsers.EventId == item.Id
                               select new EventDetailUserView
                               {
                                   Id = partici.Id,
                                   FirstName = partici.FirstName,
                                   LastName = partici.LastName,
                                   Username = partici.Username,
                                   IsHost = eventUsers.IsHost,
                               };

            eventDetailViews.Add(new EventDetailView 
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Participants = participants,
            });
        }
        return eventDetailViews;
    }

    // helper methods
    private EventDetailView GetDetail(int id)
    {
        var eventItem = _context.EventItems.Find(id);
        if (eventItem == null) throw new KeyNotFoundException("Event not found");

        var participants = from partici in _context.Users
                           join eventUsers in _context.EventUsers on partici.Id equals eventUsers.UserId
                           where eventUsers.EventId == id
                           select new EventDetailUserView
                           {
                               Id = partici.Id,
                               FirstName = partici.FirstName,
                               LastName = partici.LastName,
                               Username = partici.Username,
                               IsHost = eventUsers.IsHost,
                           };

        return new EventDetailView
        {
            Id = eventItem.Id,
            Title = eventItem.Title,
            Description = eventItem.Description,
            StartDate = eventItem.StartDate,
            EndDate = eventItem.EndDate,
            Participants = participants,
        };
    }

    private EventItem GetEvent(int id)
    {
        var eventItem = _context.EventItems.Find(id);
        if (eventItem == null) throw new KeyNotFoundException("Event not found");
        return eventItem;
    }
}