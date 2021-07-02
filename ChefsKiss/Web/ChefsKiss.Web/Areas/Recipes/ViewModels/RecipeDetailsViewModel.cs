namespace ChefsKiss.Web.Areas.Recipes.ViewModels
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class RecipeDetailsViewModel : IMapFrom<Recipe>
    {
        public string Name { get; init; }

        public string Content { get; init; }
    }
}
