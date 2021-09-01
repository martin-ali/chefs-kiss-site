namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Models.Ingredients;

    public interface IRecipeIngredientsService
    {
        IEnumerable<RecipeIngredient> Create(
            IEnumerable<Ingredient> ingredients,
            IEnumerable<IngredientServiceModel> ingredientModels,
            Recipe recipe);

        void RemoveAll(IEnumerable<RecipeIngredient> recipeIngredients);
    }
}
