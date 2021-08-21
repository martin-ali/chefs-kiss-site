namespace ChefsKiss.Web.Controllers
{
    using ChefsKiss.Web.Models.Categories;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categories;
        private readonly IRecipesService recipes;

        public CategoriesController(ICategoriesService categories, IRecipesService recipes)
        {
            this.recipes = recipes;
            this.categories = categories;

        }

        public IActionResult Details(int id)
        {
            var model = this.categories.ById<CategoryDetailsViewModel>(id);
            var recipes = this.recipes.PagedByCategoryId<RecipeListViewModel>(0, ItemsPerPage, id);
            model.Recipes = recipes;

            return this.View(model);
        }

        // Caching
        public IActionResult Explore()
        {
            return this.View();
        }
    }
}
