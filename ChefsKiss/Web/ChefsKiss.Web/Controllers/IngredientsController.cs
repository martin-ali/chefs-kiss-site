namespace ChefsKiss.Web.Controllers
{
    using ChefsKiss.Web.Models.Ingredients;
    using ChefsKiss.Web.Models.MeasurementUnits;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.ErrorMessages;
    using static ChefsKiss.Common.WebConstants;

    public class IngredientsController : Controller
    {
        private readonly IMeasurementUnitsService measurementUnits;
        private readonly IRecipesService recipes;
        private readonly IIngredientsService ingredients;

        public IngredientsController(
            IMeasurementUnitsService measurementUnits,
            IRecipesService recipes,
            IIngredientsService ingredients)
        {
            this.measurementUnits = measurementUnits;
            this.recipes = recipes;
            this.ingredients = ingredients;
        }

        [Authorize]
        public IActionResult IngredientAddForm(int id)
        {
            var units = this.measurementUnits.All<MeasurementUnitViewModel>();
            var ingredient = new IngredientFormModel
            {
                Index = id,
            };

            // FIXME: Hard-coded
            return this.PartialView("_IngredientFormPartial", ingredient);
        }

        public IActionResult Details(int id)
        {
            var ingredient = this.ingredients.ById<IngredientDetailsViewModel>(id);

            if (ingredient == null)
            {
                return this.BadRequest(InvalidParameter(nameof(ingredient)));
            }

            var recipes = this.recipes.PagedByIngredientId<RecipeListViewModel>(0, ItemsPerPage, id);
            ingredient.Recipes = recipes;

            return this.View(ingredient);
        }
    }
}
