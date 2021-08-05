namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.Models.MeasurementUnits;
    using ChefsKiss.Web.Areas.Recipes.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    [Area(RecipesArea)]
    public class IngredientsController : Controller
    {
        private readonly IMeasurementUnitsService measurementUnitsService;

        public IngredientsController(IMeasurementUnitsService measurementUnitsService)
        {
            this.measurementUnitsService = measurementUnitsService;
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
            var viewModel = new IngredientFormModel
            {
                Index = id,
            };

            // FIXME: Hard-coded
            return this.PartialView("_IngredientFormPartial", viewModel);
        }
    }
}
