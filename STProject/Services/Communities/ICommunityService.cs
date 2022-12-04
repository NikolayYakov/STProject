using Microsoft.EntityFrameworkCore;
using STProject.Data.Models;
using STProject.Models.Community;
using System;

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
        Task<List<Community>> GetPaginatedResult(List<Community> communities, int currentPage, int pageSize = 10);

        public int GetCount();

        int Create(Community community);

        IEnumerable<CommunityCatergoryServiceModel> AllCategories();

        List<Community> Search(string searchTerm);

    }
}
