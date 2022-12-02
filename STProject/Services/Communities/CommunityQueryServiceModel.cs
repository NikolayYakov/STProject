namespace STProject.Services.Communities
{
    public class CommunityQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int ComminutiesPerPage { get; init; }

        public int TotalCommunities { get; set; }

        public IEnumerable<CommunityServiceModel> Communities { get; init; }
    }
}
