using System;
using System.Linq;
using System.Threading.Tasks;

using ChefsKiss.Data.Models;

using static ChefsKiss.Common.WebConstants;

namespace ChefsKiss.Data.Seeding
{
    public class UsersSeeder : IDataSeeder
    {
        private const int UsersCount = 100;

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            var random = new Random();
            var userRole = dbContext.Roles.First(x => x.Name == UserRoleName);

            for (int i = 0; i < UsersCount; i++)
            {
                var username = $"User-{i}@seeded.com";

                var user = new ApplicationUser
                {
                    UserName = username,
                };

                user.Roles.Add(userRole);

                dbContext.Users.Add(user);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
