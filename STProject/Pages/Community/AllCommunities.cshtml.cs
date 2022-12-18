using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using STProject.Data;
using STProject.Data.Models;
using STProject.Infrastucture;
using STProject.Services.Communities;

namespace STProject.Pages.Community
{
    public class AllCommunitiesModel : PageModel
    {
        private readonly STProject.Data.STProjectContext _context;
        private readonly ICommunityService _communities;

        public AllCommunitiesModel(STProject.Data.STProjectContext context)
        {
            _context = context;
            _communities = new CommunityService(_context);
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 2;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public List<STProject.Data.Models.Community> Communities { get; set; }

        public List<STProject.Data.Models.UserToCommunity> UserToCommunities { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public string UserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public string[] Sorting = new string[] { "Date", "Alphabetical" };

        public string SortingMethod { get; set; }

        public async Task OnGetAsync()
        {
            Users = _context.Users.ToList();
            UserToCommunities = _context.UsersToCommunities.ToList();
            Communities = _communities.Search(SearchTerm);
            Count = Communities.Count();
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Communities = await _communities.GetPaginatedResult(Communities, CurrentPage, PageSize);
        }

        public IActionResult OnGetAddUserToCommunity(int id)
        {
            var result = new UserToCommunity()
            {
                ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CommunityId = id
            };

            _context.UsersToCommunities.Add(result);
            _context.SaveChanges();

            return Redirect($"./Details?id={id}");
        }

        public IActionResult OnGetRemoveUserFromCommunity(int id)
        {
            var result = new UserToCommunity()
            {
                ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CommunityId = id
            };
            _context.UsersToCommunities.Remove(result);
            _context.SaveChanges();

            return Redirect($"./AllCommunities");
        }

        public IActionResult OnGetDeleteCommunity(int id)
        {
            _context.Communities.First(c => c.Id == id).IsDeleted = true;
            _context.SaveChanges();

            return Redirect($"./AllCommunities");
        }
    }
}
