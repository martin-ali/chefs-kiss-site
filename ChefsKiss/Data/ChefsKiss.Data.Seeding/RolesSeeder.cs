namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    using Microsoft.AspNetCore.Identity;

    using static ChefsKiss.Common.WebConstants;

    public class RolesSeeder : IDataSeeder
    {
        static readonly string[] roles = new[]{
            AdministratorRoleName,
            AuthorRoleName,
            UserRoleName,
        };

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = (RoleManager<ApplicationRole>)serviceProvider.GetService(typeof(RoleManager<ApplicationRole>));

            foreach (var roleName in roles)
            {
                var role = new ApplicationRole
                {
                    Name = roleName,
                };

                await roleManager.CreateAsync(role);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
