namespace ChefsKiss.Tests.Controllers
{
    using System.Collections.Generic;

    using ChefsKiss.Common;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Models.Recipes;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;

    using Xunit;

    using static ChefsKiss.Tests.Data.Items;
    using static ChefsKiss.Common.WebConstants;

    public class RecipesPagingControllerTests
    {
        [Fact]
        public void AllShouldReturnCorrectViewWithCorrectModelAndCorrectItemsCount()
        {
            MyController<RecipesPagingController>
            .Instance()
            .WithData(RecipesWithDefaultData(10))
            .Calling(c => c.All(0))
            .ShouldReturn()
            .PartialView(v => v
                .WithModelOfType<IEnumerable<RecipeListViewModel>>()
                .Passing(r =>
                {
                    r.Should().NotBeEmpty();
                    r.Should().HaveCount(ItemsPerPage);
                }));
        }

        [Fact]
        public void ByIngredientIdShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<RecipesPagingController>
            .Instance()
            .WithData(RecipesWithDefaultData(10))
            .Calling(c => c.ByIngredientId(0, 1))
            .ShouldReturn()
            .PartialView(v => v
                .WithModelOfType<IEnumerable<RecipeListViewModel>>());
        }

        [Theory]
        [InlineData(1)]
        public void ByCategoryIdShouldReturnCorrectViewWithCorrectModelAndCorrectItemsCount(int categoryId)
        {
            MyController<RecipesPagingController>
            .Instance()
            .WithData(CategoriesWithDefaultData(10))
            .WithData(RecipesWithDefaultDataAndCategoryId(10, categoryId))
            .Calling(c => c.ByCategoryId(0, categoryId))
            .ShouldReturn()
            .PartialView(v => v
                .WithModelOfType<IEnumerable<RecipeListViewModel>>()
                .Passing(r =>
                {
                    r.Should().NotBeEmpty();
                    r.Should().HaveCount(ItemsPerPage);
                }));
        }

        [Theory]
        [InlineData("rec", 1, RecipesSortBy.Newest)]
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
            .WithData(RecipesWithDefaultDataAndCategoryId(10, 1))
            .Calling(c => c.BySearchQuery(0, query))
            .ShouldReturn()
            .PartialView(v => v
                .WithModelOfType<IEnumerable<RecipeListViewModel>>());
        }
    }
}
