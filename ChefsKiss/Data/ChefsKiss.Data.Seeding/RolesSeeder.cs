namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using ChefsKiss.Data.Models;
    using static ChefsKiss.Common.WebConstants;

    public class RolesSeeder : IDataSeeder
    {
        static readonly string[] roles = new[]{
            AdministratorRoleName,
            WriterRoleName,
            UserRoleName,
        };

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var roleName in roles)
            {
                var role = new ApplicationRole
                {
                    Name = roleName,
                };

                dbContext.Roles.Add(role);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
