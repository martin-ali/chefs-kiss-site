namespace ChefsKiss.Web.Areas.Administration.Controllers
{
    using ChefsKiss.Web.Areas.Administration.Models.Writers;
    using ChefsKiss.Web.Areas.Identity.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    [Area(AdministrationArea)]
    [Authorize(Roles = AdministratorRoleName)]
    public class WritersController : Controller
    {
        private readonly IWritersService writers;

        public WritersController(IWritersService writers)
        {
            this.writers = writers;
        }

        public IActionResult Applications()
        {
            var unapprovedWriters = this.writers.GetAllUnapproved<WriterViewModel>();

            return this.View(unapprovedWriters);
        }

        public IActionResult Approve(int id)
        {
            this.writers.Approve(id);

            return this.RedirectToAction(nameof(this.Applications));
        }

        public IActionResult Deny(int id)
        {
            this.writers.Deny(id);

            return this.RedirectToAction(nameof(this.Applications));
        }
    }
}
