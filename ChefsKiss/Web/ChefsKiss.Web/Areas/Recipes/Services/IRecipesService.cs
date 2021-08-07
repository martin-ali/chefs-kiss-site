namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

    public interface IRecipesService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetPaged<T>(int page, int itemsPerPage);

        T GetById<T>(int id);

        IEnumerable<T> ByIngredientId<T>(int id);

        IEnumerable<T> GetByAuthorId<T>(string authorId);

        Task<int> CreateAsync(RecipeFormModel input, string authorId);

        Task EditAsync(int id, RecipeFormModel input);

        T GetRandom<T>();

        // Task UpdateAsync(RecipeEditInputModel input, int RecipeId)

        // Task DeleteAsync(int RecipeId)

        // void GuaranteeRecipeHasCategory(Recipe recipe, Category category)

        // Task GuaranteeRecipeHasCategoryAsync(Recipe recipe, string categoryName)
    }
}
