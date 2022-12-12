using STProject.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STProject.Data.Models
{
    public class Comment : ICreatable, IVotable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int UpvotesCount { get; set; }
        public int DownvotesCount { get; set; }

        //ApplicationUser
        [ForeignKey("Owner")]
        public string? OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }

        //Post
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public Post? Post { get; set; }
    }
}
