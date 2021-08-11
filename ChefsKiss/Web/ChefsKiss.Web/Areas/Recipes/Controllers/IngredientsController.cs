namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.Models.MeasurementUnits;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;
    using ChefsKiss.Web.Areas.Recipes.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    [Area(RecipesArea)]
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
            // FIXME: WTF is this validation? Temporary
            var idIsValid = 0 <= id && id <= int.MaxValue;
            if (idIsValid == false)
            {
                return this.BadRequest();
            }

            var units = this.measurementUnits.GetAll<MeasurementUnitViewModel>();
            var ingredient = new IngredientFormModel
            {
                Index = id,
            };

            // FIXME: Hard-coded
            return this.PartialView("_IngredientFormPartial", ingredient);
        }

        public IActionResult Details(int id)
        {
            var ingredient = this.ingredients.ById<IngredientServiceModel>(id);
            var recipes = this.recipes.ByIngredientId<RecipeListViewModel>(id);

            var model = new IngredientDetailsViewModel
            {
                Name = ingredient.Name,
                Recipes = recipes,
            };

            return this.View(model);
        }
    }
}
