using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using STProject.Data;
using STProject.Data.Models;
using STProject.Services.Communities;

namespace STProject.Pages.Community
{
    public class AddCommunityModel : PageModel
    {
        private readonly STProject.Data.STProjectContext _context;
        private readonly ICommunityService communities;

        public AddCommunityModel(STProject.Data.STProjectContext context)
        {
            _context = context;
            communities = new CommunityService(_context);
        }

        [BindProperty]
        public STProject.Data.Models.Community Community { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            Community.Posts = new Collection<Post>();
            Community.CreatedOn = DateTime.Now;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Community.OwnerId = userId;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            communities.Create(Community);

            return RedirectToPage("/Community/AllCommunities");
        }
    }
}
