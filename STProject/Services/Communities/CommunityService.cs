using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using STProject.Data;
using STProject.Data.Models;
using STProject.Models.Community;
using System;

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

        public CommunityService(STProjectContext data)
        {
            this.data = data;
        }

        public async Task<List<Community>> GetPaginatedResult(List<Community> communities, int currentPage, int pageSize = 1)
        {
            return communities.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<Community> Search(string searchTerm = null)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return data.Communities.ToList();

            return data.Communities.Where(c => c.Name.Contains(searchTerm) ||
                                              c.Description.Contains(searchTerm) ||
                                              c.Category.Name.Contains(searchTerm)).ToList();
        }

        public int GetCount()
        {
            return this.data.Communities.Count();
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
                ComminutiesPerPage = productsPerPage
            };
        }


        public int Create(Community community)
        {
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
