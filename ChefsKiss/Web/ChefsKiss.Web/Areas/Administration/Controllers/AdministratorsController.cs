namespace ChefsKiss.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Home.Controllers;
    using ChefsKiss.Web.Areas.Identity.Controllers;
    using ChefsKiss.Web.Infrastructure.Extensions;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.Helpers;
    using static ChefsKiss.Common.WebConstants;

    [Area(AdministrationArea)]
    public class AdministratorsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdministratorsController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Become()
        {
            var user = await this.userManager.FindByIdAsync(this.User.Id());

            var result = await this.userManager.AddToRoleAsync(user, AdministratorRoleName);

            return this.RedirectToAction(nameof(UsersController.Logout), GetControllerName<HomeController>());
        }
    }
}
