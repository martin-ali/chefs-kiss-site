namespace ChefsKiss.Tests.Routing
{
    using ChefsKiss.Web.Controllers;

    using MyTested.AspNetCore.Mvc;

    using Xunit;


    public class CategoriesControllerTests
    {
        [Fact]
        public void DetailsRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/Categories/Details/1")
                    .WithUser())
               .To<CategoriesController>(c => c.Details(1));
        }

        [Fact]
        public void ExplorePostRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Categories/Explore")
                    .WithMethod(HttpMethod.Post)
                    .WithUser())
               .To<CategoriesController>(c => c.Explore());
        }
    }
}
