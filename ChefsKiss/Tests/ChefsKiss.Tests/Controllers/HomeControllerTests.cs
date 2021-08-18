namespace ChefsKiss.Tests.Controllers
{
    using ChefsKiss.Web.Areas.Home.Controllers;
    using ChefsKiss.Web.Models;

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
            .View();
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
