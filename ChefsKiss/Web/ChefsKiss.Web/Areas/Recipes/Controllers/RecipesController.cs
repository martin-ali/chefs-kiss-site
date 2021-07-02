namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using ChefsKiss.Common;
    using ChefsKiss.Web.Areas.Recipes.Services;

    using Microsoft.AspNetCore.Mvc;

    [Area(GlobalConstants.RecipesArea)]
    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;

        public RecipesController(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        public IActionResult All()
        {
            return this.View();
        }
    }
}