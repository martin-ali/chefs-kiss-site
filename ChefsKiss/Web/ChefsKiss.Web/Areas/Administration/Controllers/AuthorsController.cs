namespace ChefsKiss.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Administration.Models.Authors;
    using ChefsKiss.Web.Areas.Identity.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    [Area(AdministrationArea)]
    [Authorize(Roles = AdministratorRoleName)]
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService authors;

        public AuthorsController(IAuthorsService authors)
        {
            this.authors = authors;
        }

        public IActionResult Applications()
        {
            var unapprovedAuthors = this.authors.AllUnapproved<AuthorViewModel>();

            return this.View(unapprovedAuthors);
        }

        public async Task<IActionResult> Approve(int id)
        {
            await this.authors.Approve(id);

            return this.RedirectToAction(nameof(this.Applications));
        }

        public IActionResult Deny(int id)
        {
            this.authors.Deny(id);

            return this.RedirectToAction(nameof(this.Applications));
        }
    }
}
