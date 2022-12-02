using STProject.Models.Community;

namespace STProject.Services.Communities
{
    public interface ICommunityService
    {
        CommunityQueryServiceModel All(
            string category,
            string searchTerm,
            CommunitiesSorting sorting,
            int currentPage,
            int productsPerPage);

        int Create(
            string name,
            string description,
            DateTime createdOn,
            bool IsDeleted,
            int CategoryId,
            string OwnerId
            );

        IEnumerable<CommunityCatergoryServiceModel> AllCategories();
    }
}
