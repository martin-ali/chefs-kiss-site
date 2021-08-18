namespace ChefsKiss.Tests.Routing
{
    using ChefsKiss.Web.Areas.Recipes.Controllers;
    using ChefsKiss.Web.Areas.Recipes.Models.Reviews;

    using MyTested.AspNetCore.Mvc;

    using Xunit;


    public class ReviewsControllerTests
    {
        [Fact]
        public void CreateRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/Recipes/Reviews/Create")
                    .WithMethod(HttpMethod.Post)
                    .WithUser())
               .To<ReviewsController>(c => c.Create(With.Any<ReviewFormModel>()));
        }

        [Fact]
        public void DetailsRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Recipes/Reviews/Details/1")
                    .WithUser())
               .To<ReviewsController>(c => c.Details(1));
        }

        [Fact]
        public void DeleteRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Recipes/Reviews/Delete/1")
                    .WithQuery("recipeId", "1")
                    .WithUser())
               .To<ReviewsController>(c => c.Delete(1, 1));
        }
    }
}
