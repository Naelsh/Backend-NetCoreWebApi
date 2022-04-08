namespace WebApi.Models.Events
{
    public class UpdateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatorId { get; set; }
    }
}
