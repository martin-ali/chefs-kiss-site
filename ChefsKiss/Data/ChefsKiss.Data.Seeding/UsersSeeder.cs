namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    using Microsoft.AspNetCore.Identity;

    using static ChefsKiss.Common.WebConstants;

    public class UsersSeeder : IDataSeeder
    {
        private const int UsersCount = 100;

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            var random = new Random();
            var userManager = (UserManager<ApplicationUser>)serviceProvider.GetService(typeof(UserManager<ApplicationUser>));

            for (int i = 0; i < UsersCount; i++)
            {
                var username = $"User-{i}@seeded.com";

                var user = new ApplicationUser
                {
                    UserName = username,
                };

                await userManager.CreateAsync(user, "123456");

                await userManager.AddToRoleAsync(user, UserRoleName);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
