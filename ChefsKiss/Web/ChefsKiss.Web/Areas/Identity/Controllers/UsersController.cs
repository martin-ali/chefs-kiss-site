namespace ChefsKiss.Web.Areas.Identity.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Common;
    using ChefsKiss.Web.Areas.Home.Controllers;
    using ChefsKiss.Web.Areas.Identity.Models.Users;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;
    using ChefsKiss.Web.Areas.Recipes.Services;
    using ChefsKiss.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.Helpers;
    using static ChefsKiss.Common.WebConstants;

    [Area(IdentityArea)]
    public class UsersController : Controller
    {
        private readonly IUsersService users;
        private readonly IRecipesService recipes;

        public UsersController(IUsersService users, IRecipesService recipes)
        {
            this.recipes = recipes;
            this.users = users;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        private IActionResult RedirectToHome()
        {
            return this.RedirectToAction(nameof(HomeController.Index), GetControllerName<HomeController>());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterFormModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var result = await this.users.RegisterAsync(input.Email, input.Password);

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.View(input);
            }

            return this.RedirectToHome();
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginFormModel input)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            var result = await this.users.LoginAsync(input.Email, input.Password, input.RememberMe);

            if (result.Succeeded == false)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return this.View(input);
            }

            return this.RedirectToHome();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.users.LogoutAsync();

            return this.RedirectToHome();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return this.Details(this.User.Id());
        }

        public IActionResult Details(string id)
        {
            var recipes = this.recipes.ByAuthorId<RecipeListViewModel>(id);
            var model = this.users.GetDetails<UserDetailsViewModel>(id);

            model.RecipesCount = recipes.Count();
            model.Recipes = recipes;

            return this.View(model);
        }
    }
}
