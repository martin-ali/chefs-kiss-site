namespace ChefsKiss.Web.Areas.Recipes.Models.Recipes
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class RecipeServiceModel : IMapFrom<Recipe>
    {
        public int Id { get; init; }

        public string AuthorId { get; init; }
    }
}
