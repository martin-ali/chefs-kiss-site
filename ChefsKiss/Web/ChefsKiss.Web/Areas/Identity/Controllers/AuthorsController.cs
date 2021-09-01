namespace ChefsKiss.Web.Areas.Identity.Controllers
{
    using ChefsKiss.Web.Areas.Identity.Models.Authors;
    using ChefsKiss.Web.Areas.Identity.Services;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Infrastructure.Extensions;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.Helpers;
    using static ChefsKiss.Common.WebConstants;

    [Area(IdentityArea)]
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService authors;

        public AuthorsController(IAuthorsService authors)
        {
            this.authors = authors;
        }

        [Authorize]
        public IActionResult Apply()
        {
            var userHasApplied = this.authors.HasApplied(this.User.Id());
            if (userHasApplied)
            {
                return this.BadRequest();
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Apply(AuthorFormModel input)
        {
            var userId = this.User.Id();

            var userHasApplied = this.authors.HasApplied(userId);
            if (userHasApplied)
            {
                return this.BadRequest();
            }

            if (this.ModelState.IsValid == false)
            {
                return this.View(input);
            }

            this.authors.Create(userId, input.FirstName, input.LastName);

            return this.RedirectToAction(nameof(HomeController.Index), ControllerName<HomeController>());
        }
    }
}
