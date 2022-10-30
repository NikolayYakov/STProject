
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STProject.Data.Models;

namespace STProject.Data
{
    public class STProjectContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Post> Posts { get; set; }
        
        public STProjectContext(DbContextOptions<STProjectContext> options)
            :base(options)
        {

        }
    }
}
