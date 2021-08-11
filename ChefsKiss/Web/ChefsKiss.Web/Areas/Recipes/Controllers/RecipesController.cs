namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;
    using ChefsKiss.Web.Areas.Recipes.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.ErrorMessages;
    using static ChefsKiss.Common.WebConstants;

    [Area(RecipesArea)]
    public class RecipesController : Controller
    {
        private readonly IRecipesService recipes;
        private readonly IWritersService writers;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMeasurementUnitsService measurementUnits;

        public RecipesController(
            IRecipesService recipes,
            IWritersService writers,
            UserManager<ApplicationUser> userManager,
            IMeasurementUnitsService measurementUnits)
        {
            this.recipes = recipes;
            this.writers = writers;
            this.userManager = userManager;
            this.measurementUnits = measurementUnits;
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new RecipeFormModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RecipeFormModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            if (this.writers.IsWriter(userId) == false)
            {
                return this.Unauthorized();
            }

            var recipeId = await this.recipes.CreateAsync(input, userId);

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }

        public IActionResult List()
        {
            var recipes = this.recipes.Paged<RecipeListViewModel>(0, RecipesPerPage);

            return this.View(recipes);
        }

        public IActionResult Page(int id = 0) // FIXME: Parameter name id makes no sense in this context
        {
            var recipes = this.recipes.Paged<RecipeListViewModel>(id, RecipesPerPage);

            return this.PartialView("_PagePartial", recipes);
        }

        public IActionResult Details(int id)
        {
            var recipe = this.recipes.ById<RecipeDetailsViewModel>(id);

            var userId = this.userManager.GetUserId(this.User);
            recipe.UserHasPostedReview = recipe.Reviews.Any(x => x.AuthorId == userId);

            return this.View(recipe);
        }

        public IActionResult Random()
        {
            var recipeId = this.recipes.GetRandom<RecipeDetailsViewModel>().Id;

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = this.recipes.ById<RecipeFormModel>(id);

            var user = await this.userManager.GetUserAsync(this.User);

            if (recipe.AuthorId != user.Id)
            {
                return this.Unauthorized(NotAuthorized);
                // return this.RedirectToAction(nameof(this.Details), new { id = id });
            }

            return this.View(recipe);
        }

        // FIXME: I'm passing a web model to a service. Refactor it
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, RecipeFormModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            var recipe = this.recipes.ById<RecipeServiceModel>(id);
            var userId = this.userManager.GetUserId(this.User);

            if (recipe.AuthorId != userId)
            {
                return this.Unauthorized(NotAuthorized);
            }

            await this.recipes.EditAsync(id, model);

            return this.RedirectToAction(nameof(this.Details), new { id = id });
        }
    }
}
