
using Microsoft.EntityFrameworkCore;
using STProject.Data.Models;

namespace STProject.Data
{
    public class STProjectContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public STProjectContext(DbContextOptions<STProjectContext> options)
            :base(options)
        {

        }
    }
}
