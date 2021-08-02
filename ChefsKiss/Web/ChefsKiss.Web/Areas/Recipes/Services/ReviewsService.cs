namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.Models.Reviews;

    public class ReviewsService : IReviewsService
    {
        private readonly RecipesDbContext data;

        public ReviewsService(RecipesDbContext data)
        {
            this.data = data;
        }

        public void Create(ReviewFormModel input, string authorId)
        {
            var review = new Review
            {
                AuthorId = authorId,
                RecipeId = input.RecipeId,
                Content = input.Content,
                Rating = input.Rating,
            };

            this.data.Add(review);
            this.data.SaveChanges();
        }

        public IEnumerable<T> GetByRecipeId<T>(int id)
        {
            var reviews = this.data.Reviews
                .Where(x => x.RecipeId == id)
                .To<T>()
                .ToList();

            return reviews;
        }

        public T GetById<T>(int id)
        {
            var review = this.data.Reviews
                .Where(x => x.Id == id)
                .To<T>()
                .First();

            return review;
        }
    }
}
