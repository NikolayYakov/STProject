using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STProject.Data;
using STProject.Services.Communities;
using STProject.Services.Posts;

namespace STProject.Pages.Community;

public class AddPost : PageModel
{
    private IPostService _postService;
    private ICommunityService _communityService;

    public AddPost(STProjectContext context)
    {
        _postService = new PostService(context);
        _communityService = new CommunityService(context);
    }

    [BindProperty]
    public STProject.Data.Models.Post Post { get; set; }
    
    public int CommunityId { get; set; }
    
    public void OnGet(int communityId)
    {
        CommunityId = communityId;
    }

    public IActionResult OnPost(int communityId)
    {
        var community = _communityService.Details(communityId);
        Post.Community = community;
        _postService.Create(Post);
        return RedirectToPage("Details", new {id=communityId});
    }
}