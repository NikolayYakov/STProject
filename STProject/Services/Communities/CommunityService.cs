using AutoMapper;
using AutoMapper.QueryableExtensions;

using STProject.Data;
using STProject.Data.Models;
using STProject.Models.Community;

namespace STProject.Services.Communities
{
    public enum CommunitiesSorting
    {
        DateCreated = 0,
        Name = 1,
    }

    public class CommunityService : ICommunityService
    {
        private readonly STProjectContext data;
        private readonly AutoMapper.IConfigurationProvider mapper;

        public CommunityService(STProjectContext data, AutoMapper.IConfigurationProvider mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }


        public CommunityQueryServiceModel All(
            string category,
            string searchTerm,
            CommunitiesSorting sorting,
            int currentPage,
            int productsPerPage)
        {
            var communitiesQuery = this.data.Communities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                communitiesQuery = communitiesQuery.Where(p => p.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                communitiesQuery = communitiesQuery.Where(p =>
                    p.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            communitiesQuery = sorting switch
            {
                CommunitiesSorting.Name => communitiesQuery.OrderBy(p => p.Name),
                CommunitiesSorting.DateCreated or _ => communitiesQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = communitiesQuery.Count();

            var products = communitiesQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .ProjectTo<CommunityServiceModel>(this.mapper)
                .ToList();

            var categories = this.data
                .Communities
                .Select(c => c.Category.Name)
                .Distinct()
                .ToList();

            return new CommunityQueryServiceModel
            {
                TotalCommunities = totalProducts,
                CurrentPage = currentPage,
                Communities = products,
                ComminutiesPerPage = productsPerPage
            };
        }


        public int Create(string name, string description, DateTime createdOn, bool IsDeleted, int CategoryId, string OwnerId)
        {
            var community = new Community
            {
                Name = name,
                Description = description,
                CreatedOn = createdOn,
                IsDeleted = IsDeleted,
                CategoryId = CategoryId,
                OwnerId = OwnerId
            };

            this.data.Communities.Add(community);
            this.data.SaveChanges();

            return community.Id;
        }

        public IEnumerable<CommunityCatergoryServiceModel> AllCategories()
        => this.data
                .Categories
                .Select(c => new CommunityCatergoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
    }
}
