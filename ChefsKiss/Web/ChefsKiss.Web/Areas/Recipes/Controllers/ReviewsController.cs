namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Common;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;
    using ChefsKiss.Web.Areas.Recipes.Models.Reviews;
    using ChefsKiss.Web.Areas.Recipes.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.Helpers;
    using static ChefsKiss.Common.WebConstants;

    [Area(RecipesArea)]
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
                    GetControllerName<RecipesController>(),
                    new { id = input.RecipeId });
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var recipe = this.recipes.ById<RecipeServiceModel>(input.RecipeId);
            var reviews = this.reviews.GetByRecipeId<ReviewServiceModel>(input.RecipeId);

            var userHasCommented = reviews.Any(x => x.AuthorId == user.Id);
            var userIsRecipeAuthor = recipe.AuthorId == user.Id;

            if (userHasCommented || userIsRecipeAuthor)
            {
                return this.Unauthorized();
            }

            this.reviews.Create(input, user.Id);

            return this.RedirectToAction(
                nameof(RecipesController.Details),
                GetControllerName<RecipesController>(),
                new { id = input.RecipeId });
        }

        public IActionResult Details(int id)
        {
            var review = this.reviews.GetById<ReviewDetailsViewModel>(id);

            return this.View(review);
        }

        [Authorize(Roles = WebConstants.AdministratorRoleName)]
        public IActionResult Delete()
        {
            return this.View();
        }
    }
}
