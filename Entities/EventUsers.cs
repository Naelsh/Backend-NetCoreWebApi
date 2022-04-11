namespace WebApi.Entities
{
    public class EventUsers
    {
        public int EventId { get; set; }
        public EventItem EventItem { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsHost { get; set; }
    }
}
