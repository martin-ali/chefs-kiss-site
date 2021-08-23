namespace ChefsKiss.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Administration.Controllers;
    using ChefsKiss.Web.Areas.Administration.Models.Authors;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    using static ChefsKiss.Common.WebConstants;
    using static ChefsKiss.Tests.Data.Items;

    public class AuthorsAdministrationControllerTests
    {
        private readonly ApplicationUser User = new ApplicationUser { Id = "TestUser" };
        // private readonly Author Author = new Author { Id = 1, IsApproved = false };

        [Fact]
        public void ApplicationsShouldReturnViewWithCorrectModel()
        {
            // Only tests if the action has
            MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Administration/Authors/Applications")
                .WithUser(new[] { AdministratorRoleName }))
            .To<AuthorsController>(c => c.Applications())
            .Which()
            .ShouldReturn()
            .View(v => v.WithModelOfType<IEnumerable<AuthorViewModel>>());
        }

        [Fact]
        public void ApproveShouldApproveAuthorAndRedirectCorrectly()
        {
            MyController<AuthorsController>
            .Instance()
            .WithUser(u => u.InRole(AdministratorRoleName))
            .WithData()
            .WithData(AuthorsWithUsers(10))
            .Calling(c => c.Approve(1))
            .ShouldHave()
            .Data(data => data
                .WithSet<Author>(authors => authors
                    .Any(a => a.Id == 1 && a.IsApproved)
                ));
        }
    }
}
