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

            for (int i = 1; i <= ReviewsCount; i++)
            {
                var randomAuthor = authors[random.Next(0, authors.Count)];
                var randomRecipe = recipes[random.Next(0, recipes.Count)];
                var randomRating = random.Next(ReviewRatingMinValue, ReviewRatingMaxValue + 1);

                var review = new Review
                {
                    Content = $"Review number {i}",
                    Author = randomAuthor,
                    Recipe = randomRecipe,
                    Rating = randomRating,
                };

                dbContext.Reviews.Add(review);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
