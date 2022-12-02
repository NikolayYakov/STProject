namespace STProject.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using STProject.Models.Community;
    using STProject.Services.Communities;
    using STProject.Infrastucture;
    using AutoMapper;

    public class CommunityController : Controller
    {
        private readonly ICommunityService communities;
        private readonly IMapper mapper;

        public CommunityController(ICommunityService communities, IMapper mapper)
        {
            this.communities = communities;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult All([FromQuery] CommunitiesSearchQueryModel query)
        {

            Console.WriteLine(query.Category);

            var queryResult = this.communities.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                CommunitiesSearchQueryModel.CommunitiesPerPage
                );

            var categories = this.communities.AllCategories().Select(c => c.Name);

            query.Categories = categories;
            query.TotalCommunities = queryResult.TotalCommunities;
            query.Products = queryResult.Communities;

            return View(query);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new CommunityFormModel());

        }

        [HttpPost]
        public IActionResult Add(CommunityFormModel model)
        {
            return RedirectToAction(nameof(All));
        }
    }
}