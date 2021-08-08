namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Identity.Models.Users;

    using Microsoft.AspNetCore.Identity;

    public interface IUsersService
    {
        Task<IdentityResult> RegisterAsync(UserRegisterFormModel input);

        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);

        Task LogoutAsync();
    }
}
