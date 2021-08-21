namespace ChefsKiss.Web.Models.Ingredients
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Models.Recipes;

    public class IngredientDetailsViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public IEnumerable<RecipeListViewModel> Recipes { get; set; }
    }
}
