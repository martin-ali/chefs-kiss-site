namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.ViewModels;

    public interface IRecipesService
    {
        IEnumerable<Recipe> GetAll<T>();

        T GetById<T>(int id);

        IEnumerable<T> GetByCategory<T>(int category);

        IEnumerable<T> GetByUserId<T>(string userId);

        Task<int> CreateAsync(RecipeCreateFormModel input, string authorId);

        // Task UpdateAsync(RecipeEditInputModel input, int RecipeId)

        // Task DeleteAsync(int RecipeId)

        // void GuaranteeRecipeHasCategory(Recipe recipe, Category category)

        // Task GuaranteeRecipeHasCategoryAsync(Recipe recipe, string categoryName)
    }
}
