namespace ChefsKiss.Web.Models.Favorites
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Models.Recipes;

    public class FavoritesListViewModel : IMapFrom<Favorite>
    {
        public RecipeListViewModel Recipe { get; init; }
    }
}
