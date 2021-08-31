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

    public class AuthorsService : IAuthorsService
    {
        private readonly RecipesDbContext data;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthorsService(RecipesDbContext data, UserManager<ApplicationUser> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        public void Create(string userId, string firstName, string lastName)
        {
            var author = new Author
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
            };

            this.data.Authors.Add(author);

            this.data.SaveChanges();
        }

        public IEnumerable<T> AllUnapproved<T>()
        {
            var authors = this.data.Authors
                .Where(x => x.IsApproved == false)
                .MapTo<T>()
                .ToList();

            return authors;
        }

        public bool IsAuthor(string userId)
        {
            var isAuthor = this.data.Authors
                .Where(x => x.UserId == userId)
                .Where(x => x.IsApproved)
                .Any();

            return isAuthor;
        }

        public bool HasApplied(string userId)
        {
            var isAuthor = this.data.Authors
                .Where(x => x.UserId == userId)
                .Any();

            return isAuthor;
        }

        public async Task Approve(int id)
        {
            var author = this.data.Authors.Find(id);

            author.IsApproved = true;

            var user = this.data.Users.Find(author.UserId);
            await this.userManager.AddToRoleAsync(user, AuthorRoleName);

            this.data.SaveChanges();
        }

        public void Deny(int id)
        {
            var author = this.data.Authors.Find(id);

            this.data.Authors.Remove(author);

            this.data.SaveChanges();
        }
    }
}
