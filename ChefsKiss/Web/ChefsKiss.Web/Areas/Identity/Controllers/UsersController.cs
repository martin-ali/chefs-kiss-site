namespace ChefsKiss.Web.Areas.Identity.Controllers
{
    using System.Threading.Tasks;

    using ChefsKiss.Common;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Home.Controllers;
    using ChefsKiss.Web.Areas.Identity.Models.Users;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    [Area(IdentityArea)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> rolesManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> rolesManager)
        {
            this.userManager = userManager;
            this.rolesManager = rolesManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
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

            var user = new ApplicationUser
            {
                UserName = input.Email,
                Email = input.Email,
            };

            var result = await this.userManager.CreateAsync(user, input.Password);

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.View(input);
            }

            await this.signInManager.SignInAsync(user, false);

            // return this.RedirectToAction(Helpers.GetControllerName<HomeController>(), nameof(HomeController.Index));
            return this.RedirectToAction(Helpers.GetControllerName<HomeController>(), nameof(HomeController.Index));
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

            var result = await this.signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded == false)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return this.View(input);
            }

            return this.RedirectToAction(Helpers.GetControllerName<HomeController>(), nameof(HomeController.Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();

            return this.RedirectToAction(Helpers.GetControllerName<HomeController>(), nameof(HomeController.Index));
        }
    }
}
