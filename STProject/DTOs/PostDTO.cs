namespace STProject.DTOs
{
    public class PostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int UpvotesCount { get; set; }
        public int DownvotesCount { get; set; }
    }
}
