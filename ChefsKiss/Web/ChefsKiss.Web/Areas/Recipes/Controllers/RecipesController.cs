namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.GlobalConstants;
    using static ChefsKiss.Common.ErrorMessages;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

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

            var authorId = this.userManager.GetUserId(this.User);

            var recipeId = await this.recipesService.CreateAsync(model, authorId);

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
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

        [HttpGet]
        public IActionResult Random()
        {
            var recipeId = this.recipesService.GetRandom<RecipeDetailsViewModel>().Id;

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = this.recipesService.GetById<RecipeFormModel>(id);

            var user = await this.userManager.GetUserAsync(this.User);

            if (recipe.AuthorId != user.Id)
            {
                return this.RedirectToAction(nameof(this.Details), new { id = id });
            }

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

            var recipe = this.recipesService.GetById<Recipe>(id);
            var userId = this.userManager.GetUserId(this.User);

            if (recipe.AuthorId != userId)
            {
                return this.Unauthorized(NotAuthorized);
            }

            await this.recipesService.EditAsync(model, id);

            return this.RedirectToAction(nameof(this.Details), new { id = id });
        }
    }
}
