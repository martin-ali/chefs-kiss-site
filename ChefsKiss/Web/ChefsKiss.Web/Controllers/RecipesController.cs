namespace ChefsKiss.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Infrastructure.Extensions;
    using ChefsKiss.Web.Models.Ingredients;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.ErrorMessages;
    using static ChefsKiss.Common.Helpers;
    using static ChefsKiss.Common.WebConstants;

    public class RecipesController : Controller
    {
        private readonly IRecipesService recipes;
        private readonly IAuthorsService authors;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMeasurementUnitsService measurementUnits;

        public RecipesController(
            IRecipesService recipes,
            IAuthorsService authors,
            UserManager<ApplicationUser> userManager,
            IMeasurementUnitsService measurementUnits)
        {
            this.recipes = recipes;
            this.authors = authors;
            this.userManager = userManager;
            this.measurementUnits = measurementUnits;
        }

        public IActionResult Paged(int id) // FIXME: Parameter name id makes no sense in this context
        {
            var recipes = this.recipes.PagedAll<RecipeListViewModel>(id, ItemsPerPage);

            return this.PartialView("_PagePartialCard", recipes);
        }

        public IActionResult PagedByIngredientId(int id, int recipeId)
        {
            var recipes = this.recipes.PagedByIngredientId<RecipeListViewModel>(id, ItemsPerPage, recipeId);

            return this.PartialView("_PagePartialRow", recipes);
        }

        public IActionResult PagedBySearchTerm(int id, string searchTerm)
        {
            if (searchTerm == null)
            {
                return this.BadRequest(InvalidSearchTerm);
            }

            var recipes = this.recipes.PagedBySearchTerm<RecipeListViewModel>(id, ItemsPerPage, searchTerm);

            return this.PartialView("_PagePartialCard", recipes);
        }

        [Authorize]
        public IActionResult Create()
        {
            var isAuthor = this.authors.IsAuthor(this.User.Id());
            if (isAuthor == false)
            {
                return this.Unauthorized(MustBeAuthor);
            }

            var model = new RecipeFormModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RecipeFormModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            var isAuthor = this.authors.IsAuthor(userId);
            if (isAuthor == false)
            {
                return this.Unauthorized(MustBeAuthor);
            }

            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var ingredients = input.Ingredients.AsQueryable().MapTo<IngredientServiceModel>(); // FIXME: AsQueryable should not be necessary
            var recipeId = await this.recipes.CreateAsync(userId, input.Title, input.Content, ingredients, input.Image);

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }

        public IActionResult All()
        {
            var recipes = this.recipes.PagedAll<RecipeListViewModel>(0, ItemsPerPage);

            return this.View(recipes);
        }

        public IActionResult Search(string searchTerm)
        {
            if (searchTerm == null)
            {
                return this.BadRequest(InvalidSearchTerm);
            }

            var recipes = this.recipes.PagedBySearchTerm<RecipeListViewModel>(0, ItemsPerPage, searchTerm);
            var model = new RecipesSearchViewModel
            {
                SearchTerm = searchTerm,
                Recipes = recipes,
            };

            return this.View(model);
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