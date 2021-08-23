namespace ChefsKiss.Web.Areas.Identity.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Identity.Models.Users;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Infrastructure.Extensions;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.ErrorMessages;
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

        private IActionResult RedirectToHome()
        {
            return this.RedirectToAction(nameof(HomeController.Index), ControllerName<HomeController>());
        }

        public IActionResult Register()
        {
            return this.View();
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
        public IActionResult Profile()
        {
            return this.Details(this.User.Id());
        }

        public IActionResult Details(string id)
        {
            var recipes = this.recipes.ByAuthorId<RecipeListViewModel>(id);
            var user = this.users.Details<UserDetailsViewModel>(id);

            if (user == null)
            {
                return this.BadRequest(InvalidParameter(nameof(user)));
            }

            user.RecipesCount = recipes.Count();
            user.Recipes = recipes;

            return this.View(user);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.users.LogoutAsync();

            return this.RedirectToHome();
        }
    }
}
