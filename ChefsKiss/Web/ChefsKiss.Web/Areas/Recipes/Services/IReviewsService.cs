namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Recipes.ViewModels.Reviews;

    public interface IReviewsService
    {
        Task CreateAsync(ReviewCreateFormModel input, string authorId);
    }
}
