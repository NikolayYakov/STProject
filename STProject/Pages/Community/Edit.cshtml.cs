﻿using System;
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
    public class EditModel : PageModel
    {
        private readonly STProject.Data.STProjectContext _context;
        private readonly ICommunityService communities;

        public EditModel(STProject.Data.STProjectContext context)
        {
            _context = context;
            this.communities = new CommunityService(_context);
        }

        [BindProperty]
        public STProject.Data.Models.Community Community { get; set; } = default!;

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
            Community = community;
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Community).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunityExists(Community.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect($"/Community/Details?id={Community.Id}");
        }

        private bool CommunityExists(int id)
        {
            return _context.Communities.Any(e => e.Id == id);
        }
    }
}
