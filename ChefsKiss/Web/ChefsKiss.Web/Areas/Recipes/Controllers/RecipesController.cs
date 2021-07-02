namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using ChefsKiss.Common;
    using ChefsKiss.Web.Areas.Recipes.Services;
    using ChefsKiss.Web.Areas.Recipes.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    [Area(GlobalConstants.RecipesArea)]
    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        [HttpGet]
        public IActionResult All()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(RecipeCreateFormModel model)
        {
            return this.Redirect("Home");
        }
    }
}
