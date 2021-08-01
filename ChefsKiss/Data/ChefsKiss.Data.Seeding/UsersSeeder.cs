using System;
using System.Threading.Tasks;
using ChefsKiss.Data.Models;

namespace ChefsKiss.Data.Seeding
{
    public class UsersSeeder : IDataSeeder
    {
        private const int UsersCount = 100;

        private readonly string[] FirstNames = new[]{
            "John",
            "Judy",
            "Elizabeth",
            "William",
            "Alexander",
            "Ivan",
            "Peter",
            "Wade",
            "Dave",
            "George",
            "Riley",
            "Dan",
            "Gilbert",
            "Brian",
            "Robert",
            "Liam",
            "Lewis",
            "joshua",
            "Blake",
            "Antonio",
            "Harold",
            "Shane",
            "Hector",
            "Carlos",
            "Connor",
            "Paul",
            "Joey",
            "Troy",
            "Olivia",
            "Emma",
            "Charlotte",
            "Sophia",
            "Isabella",
            "Mia",
            "Sofia",
            "Emily",
        };

        private readonly string[] LastNames = new[]{
            "John",
            "Judy",
            "Elizabeth",
            "William",
            "Alexander",
            "Ivan",
            "Peter",
            "Wade",
            "Dave",
            "George",
            "Riley",
            "Dan",
            "Gilbert",
            "Brian",
            "Robert",
            "Liam",
            "Lewis",
            "joshua",
            "Blake",
            "Antonio",
            "Harold",
            "Shane",
            "Hector",
            "Carlos",
            "Connor",
            "Paul",
            "Joey",
            "Troy",
            "Olivia",
            "Emma",
            "Charlotte",
            "Sophia",
            "Isabella",
            "Mia",
            "Sofia",
            "Emily",
        };

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            var random = new Random();

            for (int i = 0; i < UsersCount; i++)
            {
                var username = $"User-{i}@user.com";
                var firstName = FirstNames[random.Next(0, FirstNames.Length)];
                var lastName = LastNames[random.Next(0, LastNames.Length)];

                var user = new ApplicationUser
                {
                    UserName = username,
                    FirstName = firstName,
                    LastName = lastName,
                };

                dbContext.Add(user);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
