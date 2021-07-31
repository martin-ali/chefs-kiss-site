using System.Collections.Generic;
using System.Threading.Tasks;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;

namespace ChefsKiss.Web.Areas.Recipes.Services
{
    public interface IRecipeIngredientsService
    {
        Task DeleteAllAsync(IEnumerable<RecipeIngredient> recipeIngredients);

        Task<IEnumerable<RecipeIngredient>> CreateAsync(
             IEnumerable<Ingredient> ingredients,
             IEnumerable<IngredientFormModel> ingredientModels,
             Recipe recipe);
    }
}