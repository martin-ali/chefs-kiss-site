namespace ChefsKiss.Tests.Controllers
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Identity.Controllers;
    using ChefsKiss.Web.Areas.Identity.Models.Authors;
    using ChefsKiss.Web.Controllers;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    using static ChefsKiss.Tests.Data.Items;


    public class AuthorsIdentityControllerTests
    {
        [Fact]
        public void ApplyGetShouldAllowAuthorizedAndReturnCorrectView()
        {
            MyController<AuthorsController>
            .Instance()
            .WithUser()
            .Calling(c => c.Apply())
            .ShouldHave()
            .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();
        }

        [Theory]
        [InlineData("first", "last")]
        public void ApplyPostShouldAllowAuthorizedAndReturnCorrectModelAndRedirectCorrectly(string firstName, string lastName)
        {
            var model = new AuthorFormModel
            {
                FirstName = firstName,
                LastName = lastName,
            };

            MyController<AuthorsController>
            .Instance()
            .WithUser()
            .Calling(c => c.Apply(model))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r.To<HomeController>(c => c.Index()));
        }
    }
}
