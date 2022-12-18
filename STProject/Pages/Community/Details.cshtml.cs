using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using STProject.Data;
using STProject.Data.Models;
using STProject.DTOs;
using STProject.Services.Communities;
using STProject.Services.Posts;

namespace STProject.Pages.Community
{
    public class DetailsModel : PageModel
    {
        private readonly STProject.Data.STProjectContext _context;
        private readonly ICommunityService _communities;
        private readonly IPostService _postService;

        public DetailsModel(STProject.Data.STProjectContext context)
        {
            _context = context;
            _postService = new PostService(_context);
            _communities = new CommunityService(_context);
        }

        public STProject.Data.Models.Community Community { get; set; }
        public List<Post> Posts { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Community = _communities.Details(id);
            Posts = _postService.Search(id, null);
            if (Community == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
