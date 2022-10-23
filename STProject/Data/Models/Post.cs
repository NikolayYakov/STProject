using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace STProject.Data.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [Required]
        [MaxLength(55)]
        public string Title { get; set; }
        [Required]
        [MaxLength(255)]
        public string Content { get; set; }
        public int UpvotesCount { get; set; } = 1;
        public int DownvotesCount { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }
}
