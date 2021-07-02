namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class RecipeInListViewModel : IMapFrom<Recipe>
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string AuthorId { get; init; }

        public string AuthorUsername { get; init; }
    }
}
