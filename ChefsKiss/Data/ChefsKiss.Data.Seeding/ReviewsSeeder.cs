namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    using static ChefsKiss.Common.DataConstants;

    public class ReviewsSeeder : IDataSeeder
    {
        private const int ReviewsCount = 500;

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Reviews.Any())
            {
                return;
            }

            var recipes = dbContext.Recipes.ToList();
            var authors = dbContext.Users.ToList();
            var random = new Random();

            for (int current = 1; current <= ReviewsCount; current++)
            {
                var baseContent = $"Review number {current}";
                var contentExtracharacters = new string('-', random.Next(0, (ReviewContentMaxLength - baseContent.Length)));
                var content = $"Review number {current}" + contentExtracharacters;

                var author = authors[random.Next(0, authors.Count)];
                var recipe = recipes[random.Next(0, recipes.Count)];
                var rating = random.Next(ReviewRatingMinValue, ReviewRatingMaxValue + 1);

                var review = new Review
                {
                    Content = content,
                    Author = author,
                    Recipe = recipe,
                    Rating = rating,
                };

                dbContext.Reviews.Add(review);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
