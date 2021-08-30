namespace ChefsKiss.Web.Controllers
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    public class RecipesPagingController : Controller
    {
        private readonly IRecipesService recipes;
        private readonly IAuthorsService authors;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMeasurementUnitsService measurementUnits;
        private readonly ICategoriesService categories;

        public RecipesPagingController(
             IRecipesService recipes,
             IAuthorsService authors,
             UserManager<ApplicationUser> userManager,
             IMeasurementUnitsService measurementUnits,
             ICategoriesService categories)
        {
            this.recipes = recipes;
            this.authors = authors;
            this.userManager = userManager;
            this.measurementUnits = measurementUnits;
            this.categories = categories;
        }

        public IActionResult All(int pageNumber) // FIXME: Parameter name id makes no sense in this context
        {
            var recipes = this.recipes.PagedAll<RecipeListViewModel>(pageNumber, ItemsPerPage);

            return this.PartialView("_PagePartialCard", recipes);
        }

        public IActionResult ByIngredientId(int pageNumber, int ingredientId)
        {
            var recipes = this.recipes.PagedByIngredientId<RecipeListViewModel>(pageNumber, ItemsPerPage, ingredientId);

            return this.PartialView("_PagePartialRow", recipes);
        }

        public IActionResult ByCategoryId(int pageNumber, int categoryId)
        {
            var recipes = this.recipes.PagedByCategoryId<RecipeListViewModel>(pageNumber, ItemsPerPage, categoryId);

            return this.PartialView("_PagePartialCard", recipes);
        }

        public IActionResult BySearchQuery(int pageNumber, RecipesQueryModel query)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.BadRequest();
            }

            var recipes = this.recipes.PagedBySearchQuery<RecipeListViewModel>(pageNumber, ItemsPerPage, query.SearchTerm, query.CategoryId, query.SortBy);

            return this.PartialView("_PagePartialCard", recipes);
        }
    }
}
