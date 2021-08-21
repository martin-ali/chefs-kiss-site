namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;

    public interface IReviewsService
    {
        void Create(int recipeId, string content, int rating, string authorId);

        IEnumerable<T> ByRecipeId<T>(int id);

        T ById<T>(int id);

        void Delete(int id);
    }
}
