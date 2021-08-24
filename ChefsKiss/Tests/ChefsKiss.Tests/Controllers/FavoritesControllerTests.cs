namespace ChefsKiss.Tests.Controllers
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Models.Favorites;

    using FluentAssertions;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    using static ChefsKiss.Tests.Data.Items;

    public class FavoritesControllerTests
    {
        private static ApplicationUser User = new ApplicationUser()
        {
            Id = "TestId",
            UserName = "name",
        };

        [Theory]
        [InlineData(1)]
        public void AddShouldAddFavoriteAndRedirectCorrectly(int recipeId)
        {
            MyController<FavoritesController>
            .Instance()
            .WithData(User)
            .WithUser(User.Id, User.UserName)
            .WithData(RecipeWithDefaultData())
            .Calling(c => c.Add(recipeId))
            .ShouldHave()
            .Data(data => data
                .WithSet<Favorite>(favorites => favorites
                    .Should().NotBeEmpty()))
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r.To<RecipesController>(c => c.Details(1)));
        }

        [Fact]
        public void MineShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<FavoritesController>
            .Instance()
            .WithData(FavoriteWithData(User.Id, 1))
            .WithUser()
            .Calling(c => c.Mine())
            .ShouldReturn()
            .View(v => v.WithModelOfType<IEnumerable<FavoriteListViewModel>>());
        }

        [Theory]
        [InlineData(1)]
        public void RemoveShouldRemoveFavoriteAndRedirectCorrectly(int recipeId)
        {
            var favorite = new Favorite
            {
                UserId = User.Id,
                RecipeId = recipeId,
            };

            MyController<FavoritesController>
            .Instance()
            .WithData(User)
            .WithData(favorite)
            .WithUser(User.Id, User.UserName)
            .WithData(RecipeWithDefaultData())
            .Calling(c => c.Remove(recipeId))
            .ShouldHave()
            .Data(data => data
                .WithSet<Favorite>(favorites => favorites
                    .Should().BeEmpty()))
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r.To<RecipesController>(c => c.Details(1)));
        }

    }
}
