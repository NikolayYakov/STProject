using Microsoft.EntityFrameworkCore;
using STProject.Data.Models;
using System;

namespace STProject.Services.Communities
{
    public interface ICommunityService
    {
        Task<List<Community>> GetPaginatedResult(List<Community> communities, int currentPage, int pageSize = 10);

        public int GetCount();

        int Create(Community community);


        List<Community> Search(string searchTerm);


        Community Details(int? id);
    }
}
