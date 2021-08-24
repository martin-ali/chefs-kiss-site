namespace ChefsKiss.Tests.Controllers
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Tests.Data;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Models.Ingredients;
    using ChefsKiss.Web.Models.Recipes;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    using static ChefsKiss.Common.WebConstants;
    using static ChefsKiss.Tests.Data.Items;

    public class RecipesControllerTests
    {
        [Fact]
        public void CreateShouldAllowAuthorizedAndReturnCorrectViewWithCorrectModel()
        {
            MyController<RecipesController>
            .Instance()
            .WithUser()
            .WithData(With.Default<Author>())
            .Calling(c => c.Create())
            .ShouldHave()
            .ActionAttributes(c => c.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(v => v.WithModelOfType<RecipeCreateFormModel>());
        }

        [Fact]
        public void AllShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<RecipesController>
            .Instance()
            .Calling(c => c.All())
            .ShouldReturn()
            .View(v => v.WithModelOfType<IEnumerable<RecipeListViewModel>>());
        }

        [Fact]
        public void SearchShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<RecipesController>
            .Instance()
            .Calling(c => c.Search())
            .ShouldReturn()
            .View(v => v.WithModelOfType<RecipesQueryModel>());
        }

        [Fact]
        public void DetailsShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<RecipesController>
            .Instance()
            .WithData(RecipeWithDefaultData())
            .Calling(c => c.Details(1))
            .ShouldReturn()
            .View(v => v.WithModelOfType<RecipeDetailsViewModel>());
        }

        [Fact]
        public void RandomShouldReturnAndRedirectCorrectly()
        {
            MyController<RecipesController>
            .Instance()
            .WithData(RecipeWithDefaultData())
            .Calling(c => c.Random())
            .ShouldReturn()
            .Redirect(r => r.To<RecipesController>(c => c.Details(With.Any<int>())));
        }

        [Fact]
        public void EditGetShouldReturnCorrectViewWithCorrectModelWhenAdmin()
        {
            MyController<RecipesController>
            .Instance()
            .WithUser(u => u.InRoles(AdministratorRoleName))
            .WithData(RecipeWithDefaultData())
            .Calling(c => c.Edit(1))
            .ShouldHave()
            .ActionAttributes(c => c.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(v => v.WithModelOfType<RecipeEditFormModel>());
        }

        [Fact]
        public void EditPostShouldReturnCorrectViewWithCorrectModelWhenAdmin()
        {
            var editForm = RecipeEditFormWithData();

            MyController<RecipesController>
            .Instance()
            .WithUser(u => u.InRoles(AdministratorRoleName))
            .WithData(RecipeWithDefaultData())
            .Calling(c => c.Edit(1, editForm))
            .ShouldHave()
            .ActionAttributes(c => c.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r.To<RecipesController>(c => c.Details(1)));
        }

        [Fact]
        public void DeleteGetShouldReturnCorrectViewWithCorrectModelWhenAdmin()
        {
            MyController<RecipesController>
            .Instance()
            .WithUser(u => u.InRoles(AdministratorRoleName))
            .WithData(RecipeWithDefaultData())
            .Calling(c => c.Delete(1))
            .ShouldHave()
            .ActionAttributes(c => c.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(v => v.WithModelOfType<RecipeDeleteModel>());
        }

        [Fact]
        public void DeletePostShouldReturnCorrectViewWithCorrectModelWhenAdmin()
        {
            MyController<RecipesController>
            .Instance()
            .WithUser(u => u.InRoles(AdministratorRoleName))
            .WithData(RecipeWithDefaultData())
            .Calling(c => c.DeletePost(1))
            .ShouldHave()
            .ActionAttributes(c => c.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r.To<HomeController>(c => c.Index()));
        }
    }
}
