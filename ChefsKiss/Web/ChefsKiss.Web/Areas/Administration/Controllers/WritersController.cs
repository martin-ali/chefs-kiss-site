namespace ChefsKiss.Web.Areas.Administration.Controllers
{
    using ChefsKiss.Web.Areas.Identity.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.WebConstants;

    [Area(AdministrationArea)]
    [Authorize(Roles = AdministratorRoleName)]
    public class WritersController : Controller
    {
        private readonly IWritersService writersService;

        public WritersController(IWritersService writersService)
        {
            this.writersService = writersService;
        }

        public IActionResult Applications()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Approve(int writerId)
        {
            this.writersService.Approve(writerId);

            return this.RedirectToAction(nameof(this.Applications));
        }
    }
}
