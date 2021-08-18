namespace ChefsKiss.Data.Seeding
{
	using System;
	using System.Threading.Tasks;

	using ChefsKiss.Data.Models;

	using Microsoft.AspNetCore.Identity;

	using static ChefsKiss.Common.WebConstants;

	public class UsersSeeder : IDataSeeder
	{
		private const int UsersCount = 100;
		private const string AdminEmail = "admin@seeded.com";
		private const string RegularUserEmail = "testuser@seeded.com";
		private const string UserPassword = "123456";
		private const string AdminPassword = "123456aA*";

		public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
		{
			var random = new Random();
			var userManager = (UserManager<ApplicationUser>)serviceProvider.GetService(typeof(UserManager<ApplicationUser>));

			for (int i = 0; i < UsersCount; i++)
			{
				var username = $"User{i}@seeded.com";

				var user = new ApplicationUser
				{
					UserName = username,
				};

				await userManager.CreateAsync(user, UserPassword);

				await userManager.AddToRoleAsync(user, UserRoleName);
			}

			await SeedAdmin(userManager, AdminEmail, AdminPassword);
			await SeedRegularUser(userManager, RegularUserEmail, AdminPassword);

			await dbContext.SaveChangesAsync();
		}

		private static async Task SeedAdmin(UserManager<ApplicationUser> userManager, string email, string password)
		{
			var admin = new ApplicationUser
			{
				UserName = email,
				Email = email,
			};

			await userManager.CreateAsync(admin, password);
			await userManager.AddToRoleAsync(admin, UserRoleName);
			await userManager.AddToRoleAsync(admin, AdministratorRoleName);
		}

		private static async Task SeedRegularUser(UserManager<ApplicationUser> userManager, string email, string password)
		{
			var user = new ApplicationUser
			{
				UserName = email,
				Email = email,
			};

			await userManager.CreateAsync(user, password);
			await userManager.AddToRoleAsync(user, UserRoleName);
		}
	}
}
