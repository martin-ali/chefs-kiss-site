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
        private readonly IMeasurementUnitsService measurementUnitsService;
        private readonly IRecipesService recipesService;
        private readonly IIngredientsService ingredientsService;

        public IngredientsController(
            IMeasurementUnitsService measurementUnitsService,
            IRecipesService recipesService,
            IIngredientsService ingredientsService)
        {
            this.measurementUnitsService = measurementUnitsService;
            this.recipesService = recipesService;
            this.ingredientsService = ingredientsService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult IngredientAddForm(int id)
        {
            // FIXME: WTF is this validation? Temporary
            var idIsValid = 0 <= id && id <= int.MaxValue;
            if (idIsValid == false)
            {
                return this.BadRequest();
            }

            var units = this.measurementUnitsService.GetAll<MeasurementUnitViewModel>();
            var ingredient = new IngredientFormModel
            {
                Index = id,
            };

            // FIXME: Hard-coded
            return this.PartialView("_IngredientFormPartial", ingredient);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var ingredient = this.ingredientsService.ById<IngredientServiceModel>(id);
            var recipes = this.recipesService.ByIngredientId<RecipeListModel>(id);

            var model = new IngredientDetailsModel
            {
                Name = ingredient.Name,
                Recipes = recipes,
            };

            return this.View(model);
        }
    }
}
