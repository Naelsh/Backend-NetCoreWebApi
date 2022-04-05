namespace WebApi.Entities;
public class EventItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public User Creator { get; set; }
    public List<User> Admins { get; set; }
    public List<User> Participants { get; set; }
}