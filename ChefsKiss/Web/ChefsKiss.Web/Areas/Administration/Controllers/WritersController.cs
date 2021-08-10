namespace ChefsKiss.Web.Areas.Administration.Controllers
{
    using ChefsKiss.Web.Areas.Identity.Models.Writers;
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
            var unapprovedWriters = this.writers.GetAllUnapproved<WriterServiceModel>();

            return this.View(unapprovedWriters);
        }

        [HttpPost]
        public IActionResult Approve(int writerId)
        {
            this.writers.Approve(writerId);

            return this.RedirectToAction(nameof(this.Applications));
        }
    }
}
