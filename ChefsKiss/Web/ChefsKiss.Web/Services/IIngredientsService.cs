namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;

    public interface IIngredientsService
    {
        IEnumerable<Ingredient> EnsureAll(IEnumerable<string> ingredientNames);

        T ById<T>(int id);
    }
}
