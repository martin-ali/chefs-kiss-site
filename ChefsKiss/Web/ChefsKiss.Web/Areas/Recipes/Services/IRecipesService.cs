namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes;

    public interface IRecipesService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        IEnumerable<T> GetByCategory<T>(int category);

        IEnumerable<T> GetByAuthorId<T>(string authorId);

        Task<int> CreateAsync(RecipeCreateFormModel input, string authorId);

        T GetRandom<T>();

        // Task UpdateAsync(RecipeEditInputModel input, int RecipeId)

        // Task DeleteAsync(int RecipeId)

        // void GuaranteeRecipeHasCategory(Recipe recipe, Category category)

        // Task GuaranteeRecipeHasCategoryAsync(Recipe recipe, string categoryName)
    }
}
