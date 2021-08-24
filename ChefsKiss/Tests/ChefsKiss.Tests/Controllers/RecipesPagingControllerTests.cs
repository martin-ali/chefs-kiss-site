namespace ChefsKiss.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Common;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Models.Recipes;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    using static ChefsKiss.Tests.Data.Items;

    public class RecipesPagingControllerTests
    {
        [Fact]
        public void AllShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<RecipesPagingController>
            .Instance()
            .WithData(Models<Recipe>(10))
            .Calling(c => c.All(0))
            .ShouldReturn()
            .PartialView(v => v
                .WithModelOfType<IEnumerable<RecipeListViewModel>>());
        }

        [Fact]
        public void ByIngredientIdShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<RecipesPagingController>
            .Instance()
            .WithData(Models<Recipe>(10))
            .Calling(c => c.ByIngredientId(1, 1))
            .ShouldReturn()
            .PartialView(v => v
                .WithModelOfType<IEnumerable<RecipeListViewModel>>());
        }

        [Theory]
        [InlineData(1)]
        public void ByCategoryIdShouldReturnCorrectViewWithCorrectModel(int categoryId)
        {
            MyController<RecipesPagingController>
            .Instance()
            .WithData(Models<Recipe>(10)
                .Select(r =>
                {
                    r.CategoryId = categoryId;
                    return r;
                }))
            .Calling(c => c.ByCategoryId(1, 1))
            .ShouldReturn()
            .PartialView(v => v
                .WithModelOfType<IEnumerable<RecipeListViewModel>>());
        }

        [Theory]
        [InlineData("term", 1, RecipesSortBy.Newest)]
        public void BySearchQueryShouldReturnCorrectViewWithCorrectModel(string searchTerm, int categoryId, RecipesSortBy sortBy)
        {
            var query = new RecipesQueryModel
            {
                SearchTerm = searchTerm,
                CategoryId = categoryId,
                SortBy = sortBy
            };

            MyController<RecipesPagingController>
            .Instance()
            .WithData(Models<Recipe>(10)
                .Select(r =>
                {
                    r.CategoryId = categoryId;
                    return r;
                }))
            .Calling(c => c.BySearchQuery(1, query))
            .ShouldReturn()
            .PartialView(v => v
                .WithModelOfType<IEnumerable<RecipeListViewModel>>());
        }
    }
}
