namespace ChefsKiss.Tests.Controllers
{
    using System.Collections.Generic;

    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Models;
    using ChefsKiss.Web.Models.Recipes;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void HomeShouldReturnView()
        {
            MyMvc
            .Pipeline()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index())
            .Which()
            .ShouldReturn()
            .View(v => v.WithModelOfType<IEnumerable<RecipeListViewModel>>());
        }

        [Fact]
        public void ErrorShouldReturnViewWithCorrectModel()
        {
            MyMvc
            .Pipeline()
            .ShouldMap("/Home/Error")
            .To<HomeController>(c => c.Error())
            .Which()
            .ShouldReturn()
            .View(v => v.WithModelOfType<ErrorViewModel>());
        }
    }
}
