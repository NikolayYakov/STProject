using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using STProject.Data;
using STProject.Data.Models;
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
        public int PageSize { get; set; } = 1;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public List<STProject.Data.Models.Community> Communities { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public string[] Sorting = new string[] { "Date", "Alphabetical" };

        public string SortingMethod { get; set; }

        public async Task OnGetAsync()
        {
            Communities = _communities.Search(SearchTerm);
            Count = Communities.Count();

            Communities = await _communities.GetPaginatedResult(Communities, CurrentPage, PageSize);
        }
    }
}
