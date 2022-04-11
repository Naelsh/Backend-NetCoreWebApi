namespace WebApi.Views
{
    public class PostDetailView
    {
        public int AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorUserName { get; set; }

        public int PostId { get; set; }
        public string PostContent { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
