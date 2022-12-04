using STProject.Data.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace STProject.Data.Models
{
    public class Community : ICreatable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(55)]

        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        [MaxLength(2048)]
        public string ImageUrl { get; set; }

        public bool IsPrivate { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; init; }

        //ApplicationUser
        [ForeignKey("Owner")]
        public string? OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }

        public ICollection<Post>? Posts { get; set; }
    }
}
