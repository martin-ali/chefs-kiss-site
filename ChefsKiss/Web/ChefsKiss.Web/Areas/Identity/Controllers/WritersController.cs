namespace ChefsKiss.Web.Areas.Identity.Controllers
{
    using ChefsKiss.Web.Areas.Home.Controllers;
    using ChefsKiss.Web.Areas.Identity.Models.Writers;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Infrastructure.Extensions;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.ErrorMessages;
    using static ChefsKiss.Common.Helpers;
    using static ChefsKiss.Common.WebConstants;

    [Area(IdentityArea)]
    public class WritersController : Controller
    {
        private readonly IWritersService writers;

        public WritersController(IWritersService writers)
        {
            this.writers = writers;
        }

        [Authorize]
        public IActionResult Apply()
        {
            var userHasApplied = this.writers.HasApplied(this.User.Id());
            if (userHasApplied)
            {
                return this.BadRequest(AlreadyAppliedForWriter);
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Apply(WriterFormModel input)
        {
            var userId = this.User.Id();

            var userHasApplied = this.writers.HasApplied(userId);
            if (userHasApplied)
            {
                return this.BadRequest(AlreadyAppliedForWriter);
            }

            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            this.writers.Create(userId, input.FirstName, input.LastName);

            return this.RedirectToAction(nameof(HomeController.Index), GetControllerName<HomeController>(), new { area = HomeArea });
        }
    }
}
