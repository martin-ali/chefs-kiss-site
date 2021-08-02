namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Recipes.Models.Reviews;

    public interface IReviewsService
    {
        void Create(ReviewFormModel input, string authorId);

        IEnumerable<T> GetByRecipeId<T>(int id);

        T GetById<T>(int id);
    }
}
