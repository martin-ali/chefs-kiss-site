namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

    using Microsoft.AspNetCore.Http;

    public interface IRecipesService
    {
        IEnumerable<T> All<T>();

        IEnumerable<T> PagedAll<T>(int page, int itemsPerPage);

        IEnumerable<T> PagedByIngredientId<T>(int page, int itemsPerPage, int ingredientId);

        IEnumerable<T> PagedWhere<T>(int page, int itemsPerPage, Expression<Func<Recipe, bool>> predicate);

        T ById<T>(int id);

        IEnumerable<T> ByIngredientId<T>(int id);

        IEnumerable<T> ByAuthorId<T>(string authorId);

        Task<int> CreateAsync(string userId, string title, string content, IEnumerable<IngredientServiceModel> ingredients, IFormFile image);

        Task EditAsync(int id, string userId, string title, string content, IEnumerable<IngredientServiceModel> ingredients, IFormFile image);

        void Remove(int id);

        T Random<T>();
    }
}
