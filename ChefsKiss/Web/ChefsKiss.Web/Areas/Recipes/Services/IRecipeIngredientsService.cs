namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;

    public interface IRecipeIngredientsService
    {
        IEnumerable<RecipeIngredient> Create(
            IEnumerable<Ingredient> ingredients,
            IEnumerable<IngredientServiceModel> ingredientModels,
            Recipe recipe);

        void DeleteAll(IEnumerable<RecipeIngredient> recipeIngredients);
    }
}
