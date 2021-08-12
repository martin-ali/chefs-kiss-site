namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class WritersService : IWritersService
    {
        private readonly RecipesDbContext data;

        public WritersService(RecipesDbContext data)
        {
            this.data = data;
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

        public void Approve(int id)
        {
            var writer = this.data.Writers.Find(id);

            writer.IsApproved = true;

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
