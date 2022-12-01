namespace STProject.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using STProject.Models.Community;

    public class CommunityController : Controller
    {
        public CommunityController()
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult All()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(CommunityFormModel model)
        {
            return RedirectToAction(nameof(All));
        }
    }
}