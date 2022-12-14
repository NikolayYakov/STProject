using STProject.Data.Models;
using STProject.DTOs;

namespace STProject.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        Task<ApplicationUser> GetUserInfo(string userId);
    }
}
