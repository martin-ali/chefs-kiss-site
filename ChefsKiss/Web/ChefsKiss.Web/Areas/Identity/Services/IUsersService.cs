namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public interface IUsersService
    {
        Task<IdentityResult> RegisterAsync(string email, string password);

        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);

        T GetDetails<T>(string id);

        Task LogoutAsync();
    }
}
