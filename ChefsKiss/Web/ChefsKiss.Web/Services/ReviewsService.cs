namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class ReviewsService : IReviewsService
    {
        private readonly RecipesDbContext data;

        public ReviewsService(RecipesDbContext data)
        {
            this.data = data;
        }

        public void Create(int recipeId, string content, int rating, string authorId)
        {
            var review = new Review
            {
                AuthorId = authorId,
                RecipeId = recipeId,
                Content = content,
                Rating = rating,
            };

            this.data.Add(review);
            this.data.SaveChanges();
        }

        public bool Exists(int id)
        {
            var reviewExists = this.data.Reviews.Any(r => r.Id == id);

            return reviewExists;
        }

        public IEnumerable<T> ByRecipeId<T>(int id)
        {
            var reviews = this.data.Reviews
                .Where(x => x.RecipeId == id)
                .MapTo<T>()
                .ToList();

            return reviews;
        }

        public T ById<T>(int id)
        {
            var review = this.data.Reviews
                .Where(x => x.Id == id)
                .MapTo<T>()
                .FirstOrDefault();

            return review;
        }

        public void Remove(int id)
        {
            var review = this.data.Reviews.Find(id);

            this.data.Reviews.Remove(review);

            this.data.SaveChanges();
        }
    }
}
