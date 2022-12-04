using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using STProject.Data;
using STProject.Data.Models;

namespace STProject.Pages.Community
{
    public class DetailsModel : PageModel
    {
        private readonly STProject.Data.STProjectContext _context;

        public DetailsModel(STProject.Data.STProjectContext context)
        {
            _context = context;
        }

        public STProject.Data.Models.Community Community { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Communities == null)
            {
                return NotFound();
            }

            var community = await _context.Communities.FirstOrDefaultAsync(m => m.Id == id);
            if (community == null)
            {
                return NotFound();
            }
            else
            {
                Community = community;
            }
            return Page();
        }
    }
}
