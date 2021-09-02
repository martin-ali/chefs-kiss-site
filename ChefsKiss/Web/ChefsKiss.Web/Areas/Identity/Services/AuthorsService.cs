namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Emails;
    using ChefsKiss.Services.Mapping;

    using Microsoft.AspNetCore.Identity;

    using static ChefsKiss.Common.WebConstants;

    public class AuthorsService : IAuthorsService
    {
        private readonly RecipesDbContext data;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emails;

        public AuthorsService(RecipesDbContext data,
        UserManager<ApplicationUser> userManager,
        IEmailSender emails)
        {
            this.data = data;
            this.userManager = userManager;
            this.emails = emails;
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

            var user = this.data.Users.Find(userId);
            this.emails.AuthorApplied(user.Email, $"{firstName} {lastName}");

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

            await this.emails.AuthorApproved(user.Email, $"{author.FirstName} {author.LastName}");

            this.data.SaveChanges();
        }

        public void Deny(int id)
        {
            var author = this.data.Authors.Find(id);
            var user = this.data.Users.Find(author.UserId);

            this.emails.AuthorDenied(user.Email, $"{author.FirstName} {author.LastName}");

            this.data.Authors.Remove(author);

            this.data.SaveChanges();
        }
    }
}
