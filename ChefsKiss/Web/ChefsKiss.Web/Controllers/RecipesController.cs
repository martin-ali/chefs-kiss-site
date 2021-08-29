namespace ChefsKiss.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Common;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Infrastructure.Extensions;
    using ChefsKiss.Web.Models.Categories;
    using ChefsKiss.Web.Models.Ingredients;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using static ChefsKiss.Common.ErrorMessages;
    using static ChefsKiss.Common.Helpers;
    using static ChefsKiss.Common.WebConstants;

    public class RecipesController : Controller
    {
        private readonly IRecipesService recipes;
        private readonly IAuthorsService authors;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMeasurementUnitsService measurementUnits;
        private readonly ICategoriesService categories;
        private readonly IFavoritesService favorites;

        public RecipesController(
            IRecipesService recipes,
            IAuthorsService authors,
            UserManager<ApplicationUser> userManager,
            IMeasurementUnitsService measurementUnits,
            ICategoriesService categories,
            IFavoritesService favorites)
        {
            this.recipes = recipes;
            this.authors = authors;
            this.userManager = userManager;
            this.measurementUnits = measurementUnits;
            this.categories = categories;
            this.favorites = favorites;
        }

        private IEnumerable<SelectListItem> CategoryOptions()
        {
            var options = this.categories
                .All<CategorySelectViewModel>()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });

            return options;
        }

        [Authorize]
        public IActionResult Create()
        {
            var isAuthor = this.authors.IsAuthor(this.User.Id());
            if (isAuthor == false)
            {
                return this.Unauthorized(MustBeAuthor);
            }

            var model = new RecipeCreateFormModel { Categories = CategoryOptions() };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RecipeCreateFormModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            var isAuthor = this.authors.IsAuthor(userId);
            if (isAuthor == false)
            {
                return this.Unauthorized(MustBeAuthor);
            }

            if (this.ModelState.IsValid == false)
            {
                input.Categories = CategoryOptions();

                return this.View(input);
            }

            var ingredients = input.Ingredients.AsQueryable().MapTo<IngredientServiceModel>(); // FIXME: AsQueryable should not be necessary
            var recipeId = await this.recipes.CreateAsync(userId, input.Title, input.Content, input.CategoryId, ingredients, input.Image);

            return this.RedirectToAction(nameof(this.Details), new { id = recipeId });
        }

        public IActionResult All()
        {
            var recipes = this.recipes.PagedAll<RecipeListViewModel>(0, ItemsPerPage);

            return this.View(recipes);
        }

        public IActionResult Search()
        {
            var categories = this.categories.All<CategorySelectViewModel>();
            var model = new RecipesQueryModel
            {
                Categories = categories,
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Search(RecipesQueryModel query)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(query);
            }

            var recipes = this.recipes.PagedBySearchQuery<RecipeListViewModel>(0, ItemsPerPage, query.SearchTerm, query.CategoryId, query.SortBy);
            var categories = this.categories.All<CategorySelectViewModel>();

            query.Recipes = recipes;
            query.Categories = categories;
            query.HasBeenQueried = true;

            return this.View(query);
        }

        public IActionResult Details(int id)
        {
            var recipe = this.recipes.ById<RecipeDetailsViewModel>(id);

            if (recipe == null)
            {
                var errorMessage = InvalidParameter(nameof(recipe));
                return this.NotFound(errorMessage);
            }

            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.Id();
                recipe.UserHasPostedReview = recipe.Reviews.Any(x => x.AuthorId == userId);
                recipe.UserIsAuthor = userId == recipe.AuthorId;
                recipe.IsFavorited = this.favorites.IsFavorited(this.User.Id(), id);
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
            var recipe = this.recipes.ById<RecipeEditFormModel>(id);

            if (recipe == null)
            {
                var errorMessage = InvalidParameter(nameof(recipe));
                return this.NotFound(errorMessage);
            }

            var categories = this.categories
                .All<CategorySelectViewModel>()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            recipe.Categories = categories;

            var isAuthorized = recipe.AuthorId == this.User.Id() || this.User.IsAdmin();
            if (isAuthorized == false)
            {
                return this.Unauthorized(NotAuthorized);
            }

            return this.View(recipe);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, RecipeEditFormModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var recipe = this.recipes.ById<RecipeServiceModel>(id);

            if (recipe == null)
            {
                var errorMessage = InvalidParameter(nameof(recipe));
                return this.NotFound(errorMessage);
            }

            var userId = this.User.Id();

            var isAuthorized = recipe.AuthorId == userId || this.User.IsAdmin();
            if (isAuthorized == false)
            {
                return this.Unauthorized(NotAuthorized);
            }

            // FIXME: AsQueryable is an oddity here. Find a more streamlined way
            var ingredients = input.Ingredients.AsQueryable().MapTo<IngredientServiceModel>();
            await this.recipes.EditAsync(id, userId, input.Title, input.Content, input.CategoryId, ingredients, input.Image);

            return this.RedirectToAction(nameof(this.Details), new { id = id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var recipe = this.recipes.ById<RecipeDeleteModel>(id);

            if (recipe == null)
            {
                var errorMessage = InvalidParameter(nameof(recipe));
                return this.BadRequest(errorMessage);
            }

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

            if (recipe == null)
            {
                var errorMessage = InvalidParameter(nameof(recipe));
                return this.BadRequest(errorMessage);
            }

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
