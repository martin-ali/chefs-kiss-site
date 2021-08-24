namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class FavoritesService : IFavoritesService
    {
        private readonly RecipesDbContext data;

        public FavoritesService(RecipesDbContext data)
        {
            this.data = data;
        }

        public void Add(string userId, int recipeId)
        {
            var user = this.data.Users.Find(userId);
            var recipe = this.data.Recipes.Find(recipeId);

            var favorite = new Favorite
            {
                User = user,
                Recipe = recipe,
            };

            this.data.Favorites.Add(favorite);
            this.data.SaveChanges();
        }

        public bool IsFavorited(string userId, int recipeId)
        {
            return this.data.Favorites
                .Where(f => f.UserId == userId)
                .Where(f => f.RecipeId == recipeId)
                .Any();
        }

        public IEnumerable<T> ByUserId<T>(string id)
        {
            var favorites = this.data.Favorites
                .Where(f => f.UserId == id)
                .MapTo<T>()
                .ToList();

            return favorites;
        }

        public bool CanFavorite(string userId, int recipeId)
        {
            var userIsValid = this.data.Users.Any(u => u.Id == userId);
            var recipeIsValid = this.data.Recipes.Any(u => u.Id == recipeId);
            var notFavorited = this.IsFavorited(userId, recipeId) == false;

            var canFavorite = userIsValid && recipeIsValid && notFavorited;
            return canFavorite;
        }

        public bool CanUnfavorite(string userId, int recipeId)
        {
            var userIsValid = this.data.Users.Any(u => u.Id == userId);
            var recipeIsValid = this.data.Recipes.Any(u => u.Id == recipeId);
            bool hasBeenFavorited = this.IsFavorited(userId, recipeId);

            var canUnfavorite = userIsValid && recipeIsValid && hasBeenFavorited;
            return canUnfavorite;
        }

        public void Remove(string userId, int recipeId)
        {
            var favorite = this.data.Favorites
                .Where(f => f.UserId == userId)
                .Where(f => f.RecipeId == recipeId)
                .First();

            this.data.Favorites.Remove(favorite);
            this.data.SaveChanges();
        }
    }
}
