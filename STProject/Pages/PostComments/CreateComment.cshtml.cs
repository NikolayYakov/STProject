using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using STProject.Data.Models;
using STProject.Infrastucture;
using STProject.Services.Communities;
using STProject.Services.Comments;

namespace STProject.Pages.PostComments
{
	public class CreateCommentModel : PageModel
    {
        private readonly STProject.Data.STProjectContext _context;
        private readonly ICommentService _comment;


        public CreateCommentModel(STProject.Data.STProjectContext context)
        {
            _context = context;
            _comment = new CommentsService(_context);
        }

        [BindProperty]
        public STProject.Data.Models.Comment Comment { get; set; }
        public STProject.Data.Models.ApplicationUser ApplicationUser { get; set; }

        public List<STProject.Data.Models.Comment> Comments { get; set; }


        public async Task<IActionResult> OnGetAsync()
        { 
            //Comments = await _comment.GetAllByPost(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Comment.CreatedOn = DateTime.Now;
            Comment.OwnerId = this.User.GetId();

            var comment = new Comment();

            if (await TryUpdateModelAsync<Comment>(
                comment,
                "comment",
                s => s.Content))
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return Page();
            }

                return Page();
        }
    }
}
