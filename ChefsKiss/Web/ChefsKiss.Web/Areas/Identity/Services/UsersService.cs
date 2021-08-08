namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Identity.Models.Users;

    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> rolesManager;

        public UsersService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> rolesManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.rolesManager = rolesManager;
        }

        public async Task<IdentityResult> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var result = await this.userManager.CreateAsync(user, password);

            if (result.Succeeded == false)
            {
                return result;
            }

            await this.signInManager.SignInAsync(user, false);

            return IdentityResult.Success;
        }

        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            var result = await this.signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);

            if (result.Succeeded == false)
            {
                return result;
            }

            return SignInResult.Success;
        }

        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}
