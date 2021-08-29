namespace ChefsKiss.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Models.Reviews;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.ErrorMessages;
    using static ChefsKiss.Common.Helpers;
    using static ChefsKiss.Common.WebConstants;

    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviews;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRecipesService recipes;

        public ReviewsController(
            IReviewsService reviews,
            IRecipesService recipes,
            UserManager<ApplicationUser> userManager)
        {
            this.recipes = recipes;
            this.reviews = reviews;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ReviewFormModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction(
                    nameof(RecipesController.Details),
                    ControllerName<RecipesController>(),
                    new { id = input.RecipeId });
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var recipe = this.recipes.ById<RecipeServiceModel>(input.RecipeId);

            if (recipe == null)
            {
                var errorMessage = InvalidParameter(nameof(recipe));
                return this.BadRequest(errorMessage);
            }

            var reviews = this.reviews.ByRecipeId<ReviewServiceModel>(input.RecipeId);

            var userHasCommented = reviews.Any(x => x.AuthorId == user.Id);
            var userIsRecipeAuthor = recipe.AuthorId == user.Id;

            if (userHasCommented || userIsRecipeAuthor)
            {
                return this.BadRequest(AuthorsCantReviewOwnRecipes);
            }

            this.reviews.Create(input.RecipeId, input.Content, input.Rating, user.Id);

            return this.RedirectToAction(nameof(RecipesController.Details), ControllerName<RecipesController>(), new { id = input.RecipeId });
        }

        public IActionResult Details(int id)
        {
            var review = this.reviews.ById<ReviewDetailsViewModel>(id);

            if (review == null)
            {
                var errorMessage = InvalidParameter(nameof(review));
                return this.BadRequest(errorMessage);
            }

            return this.View(review);
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(int id, int recipeId)
        {
            var reviewExists = this.reviews.Exists(id);
            if (reviewExists == false)
            {
                var errorMessage = InvalidParameter("review");
                return this.BadRequest(errorMessage);
            }

            this.reviews.Delete(id);

            return this.RedirectToAction(nameof(RecipesController.Details), ControllerName<RecipesController>(), new { id = recipeId });
        }
    }
}
