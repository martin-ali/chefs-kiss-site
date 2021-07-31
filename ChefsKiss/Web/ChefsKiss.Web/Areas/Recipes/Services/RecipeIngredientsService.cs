namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients;

    public class RecipeIngredientsService : IRecipeIngredientsService
    {
        private readonly IRepository<RecipeIngredient> recipeIngredientsRepository;

        public RecipeIngredientsService(IRepository<RecipeIngredient> recipeIngredientsRepository)
        {
            this.recipeIngredientsRepository = recipeIngredientsRepository;
        }

        public async Task<IEnumerable<RecipeIngredient>> CreateAsync(
            IEnumerable<Ingredient> ingredients,
            IEnumerable<IngredientFormModel> ingredientModels,
            Recipe recipe)
        {
            var recipeIngredients = new List<RecipeIngredient>();

            foreach (var ingredient in ingredients)
            {
                var ingredientFormData = ingredientModels.First(im => im.Name == ingredient.Name);
                var recipeIngredient = new RecipeIngredient
                {
                    Recipe = recipe,
                    Ingredient = ingredient,
                    MeasurementUnitId = ingredientFormData.MeasurementUnitId,
                    Quantity = ingredientFormData.Quantity,
                };

                recipeIngredients.Add(recipeIngredient);

                await this.recipeIngredientsRepository.AddAsync(recipeIngredient);
            }

            return recipeIngredients;
        }

        public async Task DeleteAllAsync(IEnumerable<RecipeIngredient> recipeIngredients)
        {
            foreach (var recipeIngredient in recipeIngredients)
            {
                this.recipeIngredientsRepository.Delete(recipeIngredient);
            }

            await this.recipeIngredientsRepository.SaveChangesAsync();
        }
    }
}
