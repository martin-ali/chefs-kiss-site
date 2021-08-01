namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    public class IngredientsSeeder : IDataSeeder
    {
        private const int IngredientsCount = 100;

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Ingredients.Any())
            {
                return;
            }

            for (int current = 1; current <= IngredientsCount; current++)
            {
                var ingredient = new Ingredient
                {
                    Name = $"Ingredient {current}",
                };

                dbContext.Ingredients.Add(ingredient);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
