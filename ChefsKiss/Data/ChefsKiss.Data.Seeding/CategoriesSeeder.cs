namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using ChefsKiss.Data.Models;

    public class CategoriesSeeder : IDataSeeder
    {
        static readonly string[] categories = new[]{
            "Vegan",
            "Vegetarian",
            "Fish",
            "No meat",
            "Desserts",
            "Pasta",
            "Pizza",
            "Main course",
            "Soups",
            "Salads",
            "Appetizers",
            "Others",
        };

        //
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
