namespace ChefsKiss.Web.Areas.Recipes.Models.Ingredients
{
    using System.Collections.Generic;

    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

    public class IngredientDetailsModel
    {
        public string Name { get; init; }

        public IEnumerable<RecipeListModel> Recipes { get; init; }
    }
}
