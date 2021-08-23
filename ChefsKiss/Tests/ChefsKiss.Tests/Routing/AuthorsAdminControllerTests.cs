namespace ChefsKiss.Tests.Routing
{
    using ChefsKiss.Web.Areas.Administration.Controllers;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    public class AuthorsAdminControllerTests
    {
        [Fact]
        public void ApplicationsRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap("/Administration/Authors/Applications")
               .To<AuthorsController>(c => c.Applications());
        }

        [Fact]
        public void ApproveRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap("/Administration/Authors/Approve/1")
               .To<AuthorsController>(c => c.Approve(1));
        }

        [Fact]
        public void DenyRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap("/Administration/Authors/Deny/1")
               .To<AuthorsController>(c => c.Deny(1));
        }
    }
}
