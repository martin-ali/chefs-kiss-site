namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using ChefsKiss.Data;

    public interface IDataSeeder
    {
        Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider);
    }
}
