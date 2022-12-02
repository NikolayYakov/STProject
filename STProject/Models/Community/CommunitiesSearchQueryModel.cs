using STProject.Services.Communities;
using System.ComponentModel.DataAnnotations;

namespace STProject.Models.Community
{
    public class CommunitiesSearchQueryModel
    {
        public const int CommunitiesPerPage = 6;

        public int TotalCommunities { get; set; }

        public CommunitiesSorting Sorting { get; init; }

        [Display(Name = "Search by text:")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public string Category { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<CommunityServiceModel> Products { get; set; }
    }
}
