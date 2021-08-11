namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

    public interface IRecipesService
    {
        IEnumerable<T> All<T>();

        IEnumerable<T> Paged<T>(int page, int itemsPerPage);

        T ById<T>(int id);

        IEnumerable<T> ByIngredientId<T>(int id);

        IEnumerable<T> ByAuthorId<T>(string authorId);

        Task<int> CreateAsync(RecipeFormModel input, string userId);

        Task EditAsync(int id, RecipeFormModel input);

        void Remove(int id);

        T GetRandom<T>();
    }
}
