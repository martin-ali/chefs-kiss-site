namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.Models.Reviews;

    public class ReviewsService : IReviewsService
    {
        private readonly IRepository<Review> reviewsRepository;

        public ReviewsService(IRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public async Task CreateAsync(ReviewFormModel input, string authorId)
        {
            var review = new Review
            {
                AuthorId = authorId,
                RecipeId = input.RecipeId,
                Content = input.Content,
                Rating = input.Rating,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetByRecipeId<T>(int id)
        {
            var reviews = this.reviewsRepository
                .All()
                .Where(x => x.RecipeId == id)
                .To<T>()
                .ToList();

            return reviews;
        }

        public T GetById<T>(int id)
        {
            var review = this.reviewsRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .First();

            return review;
        }
    }
}
