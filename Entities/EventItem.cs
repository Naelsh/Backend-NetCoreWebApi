using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public ICollection<EventUsers> Attendees { get; set; }
}