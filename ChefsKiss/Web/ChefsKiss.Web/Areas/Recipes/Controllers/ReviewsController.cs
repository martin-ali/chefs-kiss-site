namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Common;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Models.Reviews;
    using ChefsKiss.Web.Areas.Recipes.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.GlobalConstants;

    [Area(RecipesArea)]
    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsController(IReviewsService reviewsService, UserManager<ApplicationUser> userManager)
        {
            this.reviewsService = reviewsService;
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
                    Helpers.GetControllerName<RecipesController>(),
                    new { id = input.RecipeId });
            }

            var author = await this.userManager.GetUserAsync(this.User);

            var reviews = this.reviewsService.GetByRecipeId<ReviewServiceModel>(input.RecipeId);
            if (reviews.Any(x => x.AuthorId == author.Id))
            {
                return BadRequest();
            }

            await this.reviewsService.CreateAsync(input, author.Id);

            return this.RedirectToAction(
                nameof(RecipesController.Details),
                Helpers.GetControllerName<RecipesController>(),
                new { id = input.RecipeId });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var review = this.reviewsService.GetById<ReviewDetailsViewModel>(id);

            return this.View(review);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Delete()
        {
            return this.View();
        }
    }
}
