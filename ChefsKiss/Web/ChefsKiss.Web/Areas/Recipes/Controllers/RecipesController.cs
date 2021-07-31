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
        [Authorize]
        public IActionResult Create()
        {
            var model = new RecipeFormModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RecipeFormModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            var author = await this.userManager.GetUserAsync(this.User);

            var recipeId = await this.recipesService.CreateAsync(model, author.Id);

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }

        [HttpGet]
        public IActionResult All()
        {
            var recipes = this.recipesService.GetAll<RecipeInListViewModel>();

            return this.View(recipes);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var recipe = this.recipesService.GetById<RecipeFormModel>(id);

            return this.View(recipe);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, RecipeFormModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            var author = await this.userManager.GetUserAsync(this.User);

            await this.recipesService.EditAsync(model, id);

            return this.RedirectToAction(nameof(this.Details), new { id = id });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var recipe = this.recipesService.GetById<RecipeDetailsViewModel>(id);

            return this.View(recipe);
        }

        [HttpGet]
        public IActionResult Random()
        {
            var recipeId = this.recipesService.GetRandom<RecipeDetailsViewModel>().Id;

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }
    }
}
