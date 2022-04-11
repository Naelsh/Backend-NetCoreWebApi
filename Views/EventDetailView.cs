using WebApi.Entities;

namespace WebApi.Views
{
    public class EventDetailView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<EventDetailUserView> Participants { get; set; }
    }
}
