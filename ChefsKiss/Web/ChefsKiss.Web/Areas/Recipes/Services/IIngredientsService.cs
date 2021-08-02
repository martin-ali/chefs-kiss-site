namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;

    public interface IIngredientsService
    {
        IEnumerable<Ingredient> EnsureAll(IEnumerable<string> ingredientNames);
    }
}
