using ChefsKiss.Web.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace ChefsKiss.Tests.Routing
{
    public class FavoritesControllerTests
    {
        [Theory]
        [InlineData(1)]
        public void AddRouteShouldBeMapped(int recipeId)
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/Favorites/Add")
                    .WithQuery(nameof(recipeId), recipeId.ToString())
                    .WithUser())
               .To<FavoritesController>(c => c.Add(recipeId));
        }

        [Fact]
        public void MineRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/Favorites/Mine")
                    .WithUser())
               .To<FavoritesController>(c => c.Mine());
        }

        [Theory]
        [InlineData(1)]
        public void RemoveRouteShouldBeMapped(int recipeId)
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/Favorites/Remove")
                    .WithQuery(nameof(recipeId), recipeId.ToString())
                    .WithUser())
               .To<FavoritesController>(c => c.Remove(recipeId));
        }
    }
}
