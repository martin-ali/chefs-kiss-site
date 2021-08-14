namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Home.Controllers;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;
    using ChefsKiss.Web.Areas.Recipes.Services;
    using ChefsKiss.Web.Infrastructure.Extensions;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.ErrorMessages;
    using static ChefsKiss.Common.Helpers;
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
            var isAuthorized = this.writers.IsWriter(this.User.Id()) || User.IsAdmin();
            if (isAuthorized == false)
            {
                return this.Unauthorized(MustBeWriter);
            }

            var model = new RecipeFormModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RecipeFormModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            var isAuthorized = this.writers.IsWriter(userId) || User.IsAdmin();
            if (isAuthorized == false)
            {
                return this.Unauthorized(MustBeWriter);
            }

            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var ingredients = input.Ingredients.AsQueryable().MapTo<IngredientServiceModel>();
            var recipeId = await this.recipes.CreateAsync(userId, input.Title, input.Content, ingredients, input.Image);

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }

        public IActionResult All()
        {
            var recipes = this.recipes.PagedAll<RecipeListViewModel>(0, ItemsPerPage);

            return this.View(recipes);
        }

        public IActionResult Paged(int id) // FIXME: Parameter name id makes no sense in this context
        {
            var recipes = this.recipes.PagedAll<RecipeListViewModel>(id, ItemsPerPage);

            return this.PartialView("_PagePartial", recipes);
        }

        public IActionResult PagedByIngredientId(int id, int recipeId) // FIXME: Parameter name id makes no sense in this context
        {
            var recipes = this.recipes.PagedByIngredientId<RecipeListViewModel>(id, ItemsPerPage, recipeId);

            return this.PartialView("_PagePartial", recipes);
        }

        public IActionResult Details(int id)
        {
            var recipe = this.recipes.ById<RecipeDetailsViewModel>(id);

            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.Id();
                recipe.UserHasPostedReview = recipe.Reviews.Any(x => x.AuthorId == userId);
                recipe.UserIsAuthor = userId == recipe.AuthorId;
            }

            return this.View(recipe);
        }

        public IActionResult Random()
        {
            var recipeId = this.recipes.Random<RecipeServiceModel>().Id;

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var recipe = this.recipes.ById<RecipeFormModel>(id);

            var isAuthorized = recipe.AuthorId == this.User.Id() || this.User.IsAdmin();
            if (isAuthorized == false)
            {
                return this.Unauthorized(NotAuthorized);
            }

            return this.View(recipe);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, RecipeFormModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var recipe = this.recipes.ById<RecipeServiceModel>(id);
            var userId = this.User.Id();

            var isAuthorized = recipe.AuthorId == userId || this.User.IsAdmin();
            if (isAuthorized == false)
            {
                return this.Unauthorized(NotAuthorized);
            }

            // FIXME: AsQueryable is an oddity here. Find a more streamlined way
            var ingredients = input.Ingredients.AsQueryable().MapTo<IngredientServiceModel>();
            await this.recipes.EditAsync(id, userId, input.Title, input.Content, ingredients, input.Image);

            return this.RedirectToAction(nameof(this.Details), new { id = id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var recipe = this.recipes.ById<RecipeDeleteModel>(id);

            var isAuthorized = recipe.AuthorId == this.User.Id() || this.User.IsAdmin();
            if (isAuthorized == false)
            {
                return this.Unauthorized(NotAuthorized);
            }

            return this.View(recipe);
        }

        // FIXME: I'm passing a web model to a service. Refactor it
        [HttpPost]
        [Authorize]
        [ActionName(nameof(Delete))]
        public IActionResult DeletePost(int id)
        {
            var recipe = this.recipes.ById<RecipeServiceModel>(id);
            var isAuthorized = recipe.AuthorId == this.User.Id() || this.User.IsAdmin();
            if (isAuthorized == false)
            {
                return this.Unauthorized(NotAuthorized);
            }

            this.recipes.Remove(id);

            return this.RedirectToAction(nameof(HomeController.Index), ControllerName<HomeController>());
        }
    }
}
