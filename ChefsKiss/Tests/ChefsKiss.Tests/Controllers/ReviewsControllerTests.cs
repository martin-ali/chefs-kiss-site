namespace ChefsKiss.Tests.Controllers
{
    using System.Linq;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Models.Reviews;
    using ChefsKiss.Web.Controllers;

    using FluentAssertions;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    using static ChefsKiss.Tests.Data.Items;
    using static ChefsKiss.Common.WebConstants;

    public class ReviewsControllerTests
    {
        [Fact]
        public void CreateShouldAuthorizeUsersShouldReturnCorrectView()
        {
            MyController<ReviewsController>
            .Instance()
            .WithUser()
            .Calling(c => c.Create(With.Default<ReviewFormModel>()))
            .ShouldHave()
            .ActionAttributes(c => c.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r.To<RecipesController>(c => c.Details(0)));
        }

        [Fact]
        public void DetailsShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<ReviewsController>
            .Instance()
            .WithData(MockModels<Review>(10))
            .Calling(c => c.Details(1))
            .ShouldReturn()
            .View(v => v.WithModelOfType<ReviewDetailsViewModel>());
        }

        [Theory]
        [InlineData(1)]
        public void DeleteShouldAuthorizeAdministratorsAndReturnCorrectViewWithCorrectModel(int reviewId)
        {
            MyController<ReviewsController>
            .Instance()
            .WithUser(u => u.InRoles(AdministratorRoleName))
            .WithData(MockModels<Review>(10))
            .Calling(c => c.Delete(reviewId, 1))
            .ShouldHave()
            .ActionAttributes(c => c.RestrictingForAuthorizedRequests(AdministratorRoleName))
            .AndAlso()
            .ShouldHave()
            .Data(data => data.WithSet<Review>(set =>
            {
                var review = set.SingleOrDefault(a => a.Id == reviewId);

                review.Should().BeNull();
            }));
        }
    }
}
