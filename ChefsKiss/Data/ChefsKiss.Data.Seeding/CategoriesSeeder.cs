namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using ChefsKiss.Data.Models;

    public class CategoriesSeeder : IDataSeeder
    {
        private static readonly string[] categories = new[]{
            "Vegan",
            "Vegetarian",
            "Fish",
            "Desserts",
            "Pasta",
            "Pizza",
            "Main courses",
            "Soups",
            "Salads",
            "Appetizers",
            "Exotic",
            "Others",
        };

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var categoryName in categories)
            {
                var category = new Category
                {
                    Name = categoryName,
                };

                dbContext.Categories.Add(category);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
