namespace ChefsKiss.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Models.Ingredients;

    using Microsoft.AspNetCore.Http;

    public interface IRecipesService
    {
        Task<int> CreateAsync(string userId, string title, string content, IEnumerable<IngredientServiceModel> ingredients, IFormFile image);

        IEnumerable<T> All<T>();

        IEnumerable<T> Popular<T>(int count);

        IEnumerable<T> PagedAll<T>(int page, int itemsPerPage);

        IEnumerable<T> PagedByIngredientId<T>(int page, int itemsPerPage, int ingredientId);

        IEnumerable<T> PagedBySearchTerm<T>(int page, int itemsPerPage, string searchTerm);

        IEnumerable<T> PagedWhere<T>(int page, int itemsPerPage, Expression<Func<Recipe, bool>> predicate);

        T ById<T>(int id);

        IEnumerable<T> ByAuthorId<T>(string authorId);

        IEnumerable<T> ByIngredientId<T>(int id);

        T Random<T>();

        Task EditAsync(int id, string userId, string title, string content, IEnumerable<IngredientServiceModel> ingredients, IFormFile image);

        void Remove(int id);
    }
}
