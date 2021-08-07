namespace ChefsKiss.Web.Areas.Recipes.Models.Ingredients
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class IngredientServiceModel : IMapFrom<Ingredient>
    {
        public string Name { get; init; }
    }
}
