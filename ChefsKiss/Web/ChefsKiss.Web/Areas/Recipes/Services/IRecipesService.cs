namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

    using Microsoft.AspNetCore.Http;

    public interface IRecipesService
    {
        IEnumerable<T> All<T>();

        IEnumerable<T> Paged<T>(int page, int itemsPerPage);

        T ById<T>(int id);

        IEnumerable<T> ByIngredientId<T>(int id);

        IEnumerable<T> ByAuthorId<T>(string authorId);

        Task<int> CreateAsync(string userId, string title, string content, IEnumerable<IngredientServiceModel> ingredients, IFormFile image);

        Task EditAsync(int id, string userId, string title, string content, IEnumerable<IngredientServiceModel> ingredients, IFormFile image);

        void Remove(int id);

        T Random<T>();
    }
}
