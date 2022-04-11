using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class PostItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }

        public User Author { get; set; }
    }
}
