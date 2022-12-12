using Microsoft.AspNetCore.Identity;

namespace STProject.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
