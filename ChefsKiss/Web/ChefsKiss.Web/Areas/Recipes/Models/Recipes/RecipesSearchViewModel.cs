namespace ChefsKiss.Web.Areas.Recipes.Models.Recipes
{
    using System.Collections.Generic;

    public class RecipesSearchViewModel
    {
        public string SearchTerm { get; init; }

        public IEnumerable<RecipeListViewModel> Recipes { get; init; }
    }
}
