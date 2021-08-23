namespace ChefsKiss.Tests.Routing
{
    using ChefsKiss.Web.Controllers;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap("/Index")
               .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void SlashRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap("/")
               .To<HomeController>(c => c.Index());
        }

        [Fact]
        public void ErrorRouteShouldBeMapped()
        {
            MyRouting
            .Configuration()
            .ShouldMap("/Home/Error")
            .To<HomeController>(c => c.Error());
        }
    }
}
