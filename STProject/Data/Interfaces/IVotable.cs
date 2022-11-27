namespace STProject.Data.Interfaces
{
    public interface IVotable
    {
        int UpvotesCount { get; set; }
        int DownvotesCount { get; set; }
    }
}
