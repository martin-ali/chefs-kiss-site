namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class RecipeDetailsViewModel : IMapFrom<Recipe>
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Content { get; init; }
    }
}
