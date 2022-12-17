using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STProject.DTOs;
using STProject.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Xml.Linq;

namespace STProject.Pages
{
    public class ProfileModel : PageModel
    {
        private IUserRepository studentRepository;
        public ProfileModel(IUserRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public UserDTO UserData { get; set; }
        public List<PostDTO> UserPosts { get; set; }
        public List<CommentDTO> UserComments { get; set; }

        //public int CurrentPage { get; set; } = 1;
        //public int Count { get; set; }
        //public int PageSize { get; set; } = 10;
        //public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));


        public async Task OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userInfo = await studentRepository.GetUserInfo(userId);


            this.UserData = new UserDTO(userInfo.Email, userInfo.UserName);

            this.UserPosts = userInfo.Posts != null ? userInfo.Posts
                .Select(x=> new PostDTO {Title = x.Title,Content = x.Content, UpvotesCount = x.UpvotesCount,DownvotesCount = x.DownvotesCount })
                .ToList()
                : new List<PostDTO>();

            this.UserComments = userInfo.Comments
                .Select(x => new CommentDTO { Content = x.Content, UpvotesCount = x.UpvotesCount,  DownvotesCount = x.DownvotesCount })
                .ToList();

            this.UserData.TotalUpvotes = UserPosts.Select(x => x.UpvotesCount).Sum() + UserComments.Select(x => x.DownvotesCount).Sum();
            this.UserData.TotalDownvotes = UserPosts.Select(x => x.DownvotesCount).Sum() + UserComments.Select(x => x.DownvotesCount).Sum();

            ViewData["Email"] = this.UserData.Email;
            ViewData["Username"] = this.UserData.Username;
            ViewData["TotalUpvotes"] = this.UserData.TotalUpvotes;
            ViewData["TotalDownvotes"] = this.UserData.TotalDownvotes;

            //ViewData["PostTitle"] = UserPosts.FirstOrDefault()?.Title;

        }
    }
}
