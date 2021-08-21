namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Models.Ingredients;

    public class RecipeIngredientsService : IRecipeIngredientsService
    {
        private readonly RecipesDbContext data;

        public RecipeIngredientsService(RecipesDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<RecipeIngredient> Create(
            IEnumerable<Ingredient> ingredients,
            IEnumerable<IngredientServiceModel> ingredientModels,
            Recipe recipe)
        {
            var recipeIngredients = new List<RecipeIngredient>();

            foreach (var ingredient in ingredients)
            {
                var ingredientFormData = ingredientModels.First(x => x.Name == ingredient.Name);
                var recipeIngredient = new RecipeIngredient
                {
                    Recipe = recipe,
                    Ingredient = ingredient,
                    MeasurementUnitId = ingredientFormData.MeasurementUnitId,
                    Quantity = ingredientFormData.Quantity,
                };

                recipeIngredients.Add(recipeIngredient);

                this.data.RecipeIngredients.Add(recipeIngredient);
            }

            return recipeIngredients;
        }

        public void DeleteAll(IEnumerable<RecipeIngredient> recipeIngredients)
        {
            foreach (var recipeIngredient in recipeIngredients)
            {
                this.data.RecipeIngredients.Remove(recipeIngredient);
            }

            this.data.SaveChanges();
        }
    }
}
