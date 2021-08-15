namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Recipes.Models.Reviews;

    public interface IReviewsService
    {
        void Create(int recipeId, string content, int rating, string authorId);

        IEnumerable<T> ByRecipeId<T>(int id);

        T ById<T>(int id);

        void Delete(int id);
    }
}
