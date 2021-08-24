using System.Collections.Generic;

namespace ChefsKiss.Web.Services
{
    public interface IFavoritesService
    {
        void Add(string userId, int recipeId);

        bool IsFavorited(string userId, int recipeId);

        IEnumerable<T> ByUserId<T>(string id);

        bool CanFavorite(string userId, int recipeId);

        bool CanUnfavorite(string userId, int recipeId);

        void Remove(string userId, int recipeId);
    }
}
