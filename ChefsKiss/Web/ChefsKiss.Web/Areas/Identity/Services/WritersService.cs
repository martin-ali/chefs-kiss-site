namespace ChefsKiss.Web.Areas.Identity.Services
{
    using System.Linq;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;

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

        public bool IsWriter(string userId)
        {
            var isWriter = this.data.Writers
                .Where(x => x.UserId == userId)
                .Where(x => x.IsApproved)
                .Any();

            return isWriter;
        }

        public void Approve(int id)
        {
            var application = this.data.Writers.Find(id);

            application.IsApproved = true;

            this.data.SaveChanges();
        }
    }
}
