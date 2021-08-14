namespace ChefsKiss.Web.Infrastructure.Extensions
{
    using ChefsKiss.Data.Models;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IdentityBuilder AddIdentity(this IServiceCollection services)
        {
            var builder = services
            .AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            return builder;
        }

        public static IServiceCollection ConfigureCustomLoginRoute(this IServiceCollection services)
        {
            services.PostConfigure<CookieAuthenticationOptions>(
                IdentityConstants.ApplicationScheme,
                opt =>
                {
                    // FIXME: Hard-coded
                    opt.LoginPath = "/Identity/Users/Login";
                    opt.LogoutPath = "/Identity/Users/Logout";
                });

            return services;
        }
    }
}
