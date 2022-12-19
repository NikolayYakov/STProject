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
using STProject.Services.Posts;
using System.Security.Claims;
using STProject.Repositories;

namespace STProject.Pages.PostComments
{
	public class CreateCommentModel : PageModel
    {
        private readonly STProject.Data.STProjectContext _context;
        private readonly ICommentService _comment;
        private readonly IPostService _post;
        private readonly UserRepo userRepo;


        public CreateCommentModel(STProject.Data.STProjectContext context)
        {
            _context = context;
            _comment = new CommentsService(_context);
            _post = new PostService(_context);
        }

        [BindProperty]
        public STProject.Data.Models.Comment Comment { get; set; }
        public STProject.Data.Models.Post Post { get; set; }

        public List<ApplicationUser> Users { get; set; }
        public string UserId { get; set; }

        public List<STProject.Data.Models.Comment> Comments { get; set; }

        public int PostId { get; set; }



        public async Task<IActionResult> OnGetAsync(int postId)
        {
            Users = _context.Users.ToList();
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            PostId = postId;
            Comment = _comment.commentDetails(postId);
            Comments = _comment.GetAllByPost(postId);
            if(Comments == null)
            {
                return Page();
            }
            Post = _post.Details(postId);
            if(Post == null)
            {
                return Page();
            }
            if (Comment == null)
            {
                return Page();
            }
            return Page();
        }

        public IActionResult OnPostAsync(int postId)
        {
            Comment.CreatedOn = DateTime.Now;
            Comment.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Post = _post.Details(postId);
            Comment.Post = Post;
            Comment.PostId = postId;

            var comment = new Comment();

            _comment.Create(Comment);
            return  RedirectToPage("/PostComments/CreateComment", new { postId = postId });
        }
    }
}
