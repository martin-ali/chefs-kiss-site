namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Services;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.MeasurementUnits;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.GlobalConstants;

    [Area(RecipesArea)]
    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMeasurementUnitsService measurementUnitsService;

        public RecipesController(
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager,
            IMeasurementUnitsService measurementUnitsService)
        {
            this.recipesService = recipesService;
            this.userManager = userManager;
            this.measurementUnitsService = measurementUnitsService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var recipes = this.recipesService.GetAll<RecipeInListViewModel>();

            return this.View(recipes);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var recipe = this.recipesService.GetById<RecipeDetailsViewModel>(id);

            return this.View(recipe);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RecipeCreateFormModel model)
        {
            // FIXME: Make it show a proper error message
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            var author = await this.userManager.GetUserAsync(this.User);

            var recipeId = await this.recipesService.CreateAsync(model, author.Id);

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }

        [Authorize]
        [HttpGet]
        public IActionResult IngredientAddForm(int id)
        {
            // FIXME: WTF is this validation? Temporary
            var idIsValid = 0 <= id && id <= int.MaxValue;
            if (idIsValid == false)
            {
                return this.BadRequest();
            }

            var units = this.measurementUnitsService.GetAll<MeasurementUnitViewModel>();
            var viewModel = new IngredientFormDataModel
            {
                Index = id,
                MeasurementUnitOptions = units,
            };

            // FIXME: Hard-coded
            return this.PartialView("_IngredientAddForm", viewModel);
        }
    }
}
