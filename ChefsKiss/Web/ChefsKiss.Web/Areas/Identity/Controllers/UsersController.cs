namespace ChefsKiss.Web.Areas.Identity.Controllers
{
    using System.Threading.Tasks;

    using ChefsKiss.Common;
    using ChefsKiss.Web.Areas.Home.Controllers;
    using ChefsKiss.Web.Areas.Identity.Models.Users;
    using ChefsKiss.Web.Areas.Identity.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;
    using static ChefsKiss.Common.Helpers;

    [Area(IdentityArea)]
    public class UsersController : Controller
    {
        private readonly IUsersService users;

        public UsersController(IUsersService users)
        {
            this.users = users;
        }

        [HttpGet]
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

        [HttpGet]
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.users.LogoutAsync();

            return this.RedirectToHome();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            return this.View();
        }
    }
}
