using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STProject.Data.Models
{ 
    public class UserToCommunity
    {
        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey("Community")]
        public int? CommunityId { get; set; }
        public Community? Community { get; set; }
    }
}
