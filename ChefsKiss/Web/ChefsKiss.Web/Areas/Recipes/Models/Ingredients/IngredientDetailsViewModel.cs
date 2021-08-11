namespace ChefsKiss.Web.Areas.Recipes.Models.Ingredients
{
    using System.Collections.Generic;

    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

    public class IngredientDetailsViewModel
    {
        public string Name { get; init; }

        public IEnumerable<RecipeListViewModel> Recipes { get; init; }
    }
}
