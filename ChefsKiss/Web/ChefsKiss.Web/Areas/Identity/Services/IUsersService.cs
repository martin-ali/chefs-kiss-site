namespace ChefsKiss.Web.Areas.Identity.Services
{
    using ChefsKiss.Web.Areas.Identity.ViewModels;

    public interface IUsersService
    {
        UserDetailsViewModel GetProfileById(string id);
    }
}
