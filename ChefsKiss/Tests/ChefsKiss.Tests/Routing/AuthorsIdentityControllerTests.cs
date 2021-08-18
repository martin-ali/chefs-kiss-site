namespace ChefsKiss.Tests.Routing
{
    using ChefsKiss.Web.Areas.Identity.Controllers;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    public class AuthorsIdentityControllerTests
    {
        [Fact]
        public void ApplyGetRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/Identity/Authors/Apply")
                    .WithUser())
               .To<AuthorsController>(c => c.Apply());
        }

        [Fact]
        public void ApplyPostRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Identity/Authors/Apply")
                    .WithMethod(HttpMethod.Post)
                    .WithUser())
               .To<AuthorsController>(c => c.Apply());
        }
    }
}
