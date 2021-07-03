namespace ChefsKiss.Web.Areas.Recipes.Controllers
{
    using System.Threading.Tasks;

    using ChefsKiss.Common;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Services;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Comments;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area(GlobalConstants.RecipesArea)]
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentCreateFormModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction(
                    nameof(RecipesController.Details),
                    Helpers.GetControllerName<RecipesController>(),
                    new
                    {
                        id = input.RecipeId,
                    });
            }

            var author = await this.userManager.GetUserAsync(this.User);

            await this.commentsService.CreateAsync(input, author.Id);

            return this.RedirectToAction(
                nameof(RecipesController.Details),
                Helpers.GetControllerName<RecipesController>(),
                new
                {
                    id = input.RecipeId,
                });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(CommentCreateFormModel input)
        {
            return this.View();
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Delete()
        {
            return this.View();
        }
    }
}
