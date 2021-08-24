namespace ChefsKiss.Tests.Routing
{
    using ChefsKiss.Common;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Models.Recipes;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    public class RecipesPagingControllerTests
    {
        [Fact]
        public void AllRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap("/RecipesPaging/All/1")
               .To<RecipesPagingController>(c => c.All(1));
        }

        [Fact]
        public void ByIngredientIdRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/RecipesPaging/ByIngredientId/1")
                    .WithQuery("ingredientId", "1"))
               .To<RecipesPagingController>(c => c.ByIngredientId(1, 1));
        }

        [Fact]
        public void ByCategoryIdRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/RecipesPaging/ByCategoryId/1")
                    .WithQuery("categoryId", "1"))
               .To<RecipesPagingController>(c => c.ByCategoryId(1, 1));
        }

        [Fact]
        public void BySearchQueryRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/RecipesPaging/BySearchQuery/1")
                    .WithQuery(With.Any<RecipesQueryModel>()))
               .To<RecipesPagingController>(c => c.BySearchQuery(1, With.Any<RecipesQueryModel>()));
        }
    }
}
