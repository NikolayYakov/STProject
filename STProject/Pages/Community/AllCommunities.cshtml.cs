using System;
using System.Collections.Generic;
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
        private readonly ICommunityService communities;


        public AllCommunitiesModel(STProject.Data.STProjectContext context)
        {
            _context = context;
            communities = new CommunityService(_context);
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

        public void OnGetSearch()
        {
            Communities = communities.Search(SearchTerm);
        }

        public async Task OnGetAsync()
        {
            //CurrentPage = currentPage == 0 ? CurrentPage : currentPage;

            Communities = communities.Search(SearchTerm);

            Communities = await communities.GetPaginatedResult(Communities, CurrentPage, PageSize);
            Count = _context.Communities.Count();
        }


    }
}
