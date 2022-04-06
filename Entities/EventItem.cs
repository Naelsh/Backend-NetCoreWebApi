using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities;
public class EventItem
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }

    //[Required]
    //public User Creator { get; set; }
    //public List<User> Admins { get; set; } = new List<User>();
    //public List<User> Participants { get; set; } = new List<User>();
}