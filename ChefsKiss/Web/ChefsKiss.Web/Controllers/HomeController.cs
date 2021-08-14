namespace ChefsKiss.Web.Areas.Home.Controllers
{
    using System.Diagnostics;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Areas.Recipes.Services;
    using ChefsKiss.Web.Models;

    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    public class HomeController : Controller
    {
        private readonly IRecipesService recipes;
        private readonly IReviewsService reviews;
        private readonly IWritersService writers;
        private readonly IUsersService users;

        public HomeController(
            IRecipesService recipes,
            IReviewsService reviews,
            IWritersService writers,
            IUsersService users)
        {
            this.recipes = recipes;
            this.reviews = reviews;
            this.writers = writers;
            this.users = users;
        }

        [HttpGet("/")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
