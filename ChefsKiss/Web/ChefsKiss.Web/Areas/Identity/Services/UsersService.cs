namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Emails;
    using ChefsKiss.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    using static ChefsKiss.Common.WebConstants;

    public class UsersService : IUsersService
    {
        private readonly RecipesDbContext data;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emails;

        public UsersService(
            RecipesDbContext data,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailSender emails)
        {
            this.data = data;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.emails = emails;
        }

        public async Task<IdentityResult> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var result = await this.userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, UserRoleName);
                await this.signInManager.SignInAsync(user, false);
                await this.emails.Welcome(user.Email, user.UserName);
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            var result = await this.signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);

            return result;
        }

        public T Details<T>(string id)
        {
            var user = this.data.Users
                .Where(x => x.Id == id)
                .MapTo<T>()
                .FirstOrDefault();

            return user;
        }

        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}
