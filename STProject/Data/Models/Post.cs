using Microsoft.EntityFrameworkCore;
using STProject.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STProject.Data.Models
{
    public class Post : ICreatable, IVotable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(55)]
        public string Title { get; set; }
        [Required]
        [MaxLength(255)]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int UpvotesCount { get; set; }
        public int DownvotesCount { get; set; }

        // ApplicationUser
        [ForeignKey("Owner")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? Owner { get; set; }

        //Community
        [ForeignKey("Community")]
        public int? CommunityId { get; set; }
        public Community? Community { get; set; }

        public ICollection<Comment>? Comments { get; set; }

    }
}
