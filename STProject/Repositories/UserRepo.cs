using Microsoft.EntityFrameworkCore;
using STProject.Data;
using STProject.Data.Models;
using STProject.DTOs;
using STProject.Repositories.Interfaces;

namespace STProject.Repositories
{
    public class UserRepo : IUserRepository
    {
        private STProjectContext context;
        public UserRepo(STProjectContext context)
        {
            this.context = context;
        }

        public async Task<ApplicationUser?> GetUserInfo(string userId)
        {
            return await context.Users.Include(x => x.Posts.Where(x=>x.IsDeleted == false).OrderByDescending(x=>x.CreatedOn).Take(10))
                .Include(x=>x.Comments.Where(x=>x.IsDeleted == false).OrderByDescending(x=>x.CreatedOn).Take(10))
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(x=>x.Id== userId);
            //var posts = result.Posts;
        }
    }
}
