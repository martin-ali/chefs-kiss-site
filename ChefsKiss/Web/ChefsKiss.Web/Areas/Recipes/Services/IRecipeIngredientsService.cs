namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;

    public interface IRecipeIngredientsService
    {
        void DeleteAll(IEnumerable<RecipeIngredient> recipeIngredients);

        IEnumerable<RecipeIngredient> Create(
             IEnumerable<Ingredient> ingredients,
             IEnumerable<IngredientFormModel> ingredientModels,
             Recipe recipe);
    }
}
