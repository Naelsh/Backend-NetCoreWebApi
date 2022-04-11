namespace WebApi.Entities;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Username { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    public ICollection<EventUsers> Events { get; set; }
    public ICollection<PostItem> Posts { get; set; }
}