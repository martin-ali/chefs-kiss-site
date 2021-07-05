namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Threading.Tasks;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Reviews;

    public class ReviewsService : IReviewsService
    {
        private readonly IRepository<Review> reviewsRepository;

        public ReviewsService(IRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public async Task CreateAsync(ReviewCreateFormModel input, string authorId)
        {
            var review = new Review
            {
                AuthorId = authorId,
                RecipeId = input.RecipeId,
                Content = input.Content,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }
    }
}
