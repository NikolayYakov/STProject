using System.ComponentModel.DataAnnotations;

namespace STProject.Models.Community
{
    public class CommunityFormModel
    {
        [Required]
        [StringLength(55, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<CommunityCatergoryServiceModel> Categories { get; set; }
    }
}
