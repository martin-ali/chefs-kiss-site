namespace ChefsKiss.Web.Areas.Identity.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    // using ChefsKiss.Common;
    using ChefsKiss.Web.Areas.Identity.Services;
    using System;

    // using ChefsKiss.Web.Controllers;

    // [Area(GlobalConstants.IdentityArea)]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Register()
        {
            throw new NotImplementedException();
        }

        public IActionResult Login()
        {
            throw new NotImplementedException();
        }

        public IActionResult Details(string id)
        {
            var user = this.usersService.GetProfileById(id);

            return this.View(user);
        }

        public IActionResult Logout()
        {
            throw new NotImplementedException();
        }
    }
}
