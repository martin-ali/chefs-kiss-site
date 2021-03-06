namespace ChefsKiss.Web.Controllers
{
    using ChefsKiss.Web.Infrastructure.Extensions;
    using ChefsKiss.Web.Models.Favorites;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ChefsKiss.Common.Helpers;

    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoritesService favorites;

        public FavoritesController(IFavoritesService favorites)
        {
            this.favorites = favorites;
        }

        public IActionResult Add(int recipeId)
        {
            var userId = this.User.Id();

            if (this.favorites.CanFavorite(userId, recipeId) == false)
            {
                return this.BadRequest();
            }

            this.favorites.Create(userId, recipeId);

            return this.RedirectToAction(nameof(RecipesController.Details), ControllerName<RecipesController>(), new { id = recipeId });
        }

        public IActionResult Mine()
        {
            var userId = this.User.Id();

            var favorites = this.favorites.ByUserId<FavoriteListViewModel>(userId);

            return this.View(favorites);
        }

        public IActionResult Remove(int recipeId)
        {
            var userId = this.User.Id();

            if (this.favorites.CanUnfavorite(userId, recipeId) == false)
            {
                return this.BadRequest();
            }

            this.favorites.Remove(userId, recipeId);

            return this.RedirectToAction(nameof(RecipesController.Details), ControllerName<RecipesController>(), new { id = recipeId });
        }
    }
}
