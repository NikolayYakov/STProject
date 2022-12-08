using Microsoft.EntityFrameworkCore;
using STProject.Data.Models;
using System;

namespace STProject.Services.Communities
{
    public interface ICommunityService
    {
        Task<List<Community>> GetPaginatedResult(List<Community> communities, int currentPage, int pageSize = 10);

        public int GetCount();

        public int Create(Community community);

        public int Edit(Community community);

        public List<Community> Search(string searchTerm);

        public Community Details(int? id);
    }
}
