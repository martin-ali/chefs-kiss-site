namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System;
    using System.Linq;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    // using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Identity.ViewModels;

    public class UsersService : IUsersService
    {
        private readonly IRepository<ApplicationUser> usersRepository;

        public UsersService(IRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public UserDetailsViewModel GetProfileById(string id)
        {
            throw new NotImplementedException();

            // var user = this.usersRepository
            //     .AllAsNoTracking()
            //     .Where(u => u.Id == id)
            //     .To<UserDetailsViewModel>()
            //     .FirstOrDefault();

            // return user;
        }
    }
}
