namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    public interface IIngredientsService
    {
        Task<IEnumerable<Ingredient>> EnsureAllAsync(IEnumerable<string> ingredientNames);
    }
}
