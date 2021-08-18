namespace ChefsKiss.Web.Areas.Home.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;
    using ChefsKiss.Web.Areas.Recipes.Services;
    using ChefsKiss.Web.Models;

    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    public class HomeController : Controller
    {
        private readonly IRecipesService recipes;
        private readonly IReviewsService reviews;
        private readonly IAuthorsService authors;
        private readonly IUsersService users;

        public HomeController(
            IRecipesService recipes,
            IReviewsService reviews,
            IAuthorsService authors,
            IUsersService users)
        {
            this.recipes = recipes;
            this.reviews = reviews;
            this.authors = authors;
            this.users = users;
        }

        [HttpGet("/")]
        [HttpGet("/Index")]
        public IActionResult Index()
        {
            var recipes = this.recipes
            .All<RecipeListViewModel>()
            .OrderByDescending(x => x.Rating)
            .Take(5);

            return this.View(recipes);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
