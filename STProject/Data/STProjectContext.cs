
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STProject.Data.Models;
using System.Reflection.Emit;

namespace STProject.Data
{
    public class STProjectContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<UserToCommunity> UsersToCommunities { get; set; }
        public DbSet<Category> Categories { get; set; }

        public STProjectContext(DbContextOptions<STProjectContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserToCommunity>()
          .HasKey(o => new { o.ApplicationUserId, o.CommunityId });
        }
    }
}
