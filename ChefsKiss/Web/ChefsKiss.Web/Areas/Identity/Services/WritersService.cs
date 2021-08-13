namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    using Microsoft.AspNetCore.Identity;

    using static ChefsKiss.Common.WebConstants;

    public class WritersService : IWritersService
    {
        private readonly RecipesDbContext data;
        private readonly UserManager<ApplicationUser> userManager;

        public WritersService(RecipesDbContext data, UserManager<ApplicationUser> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        public void Create(string userId, string firstName, string lastName)
        {
            var writer = new Writer
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
            };

            this.data.Writers.Add(writer);

            this.data.SaveChanges();
        }

        public IEnumerable<T> GetAllUnapproved<T>()
        {
            var writers = this.data.Writers
                .Where(x => x.IsApproved == false)
                .MapTo<T>()
                .ToList();

            return writers;
        }

        public bool IsWriter(string userId)
        {
            var isWriter = this.data.Writers
                .Where(x => x.UserId == userId)
                .Where(x => x.IsApproved)
                .Any();

            return isWriter;
        }

        public bool HasApplied(string userId)
        {
            var isWriter = this.data.Writers
                .Where(x => x.UserId == userId)
                .Any();

            return isWriter;
        }

        public async Task Approve(int id)
        {
            var writer = this.data.Writers.Find(id);

            writer.IsApproved = true;

            var user = this.data.Users.Find(writer.UserId);
            var result = await this.userManager.AddToRoleAsync(user, WriterRoleName);

            this.data.SaveChanges();
        }

        public void Deny(int id)
        {
            var writer = this.data.Writers.Find(id);

            this.data.Writers.Remove(writer);

            this.data.SaveChanges();
        }
    }
}
