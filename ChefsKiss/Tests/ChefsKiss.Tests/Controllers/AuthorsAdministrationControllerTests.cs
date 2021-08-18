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
			var author = new Author { Id = 1, IsApproved = false, User = User };

			// MyPipeline
			// .Configuration()
			// .ShouldMap(request => request
			//     .WithPath("/Administration/Authors/Approve/1")
			//     .WithUser(new[] { AdministratorRoleName }))
			// .To<AuthorsController>(c => c.Approve(1))
			// .Which()
			// .ShouldReturn()
			// .Redirect(redirect => redirect.To<AuthorsController>(c => c.Applications()));

			// MyController<AuthorsController>
			// .Instance(i => i
			//     .WithData(author)
			//     .WithUser(new[] { AdministratorRoleName }))
			// .Calling(c => c.Approve(author.Id))
			// .ShouldHave()
			// .Data(d => d.WithSet<Author>(a => a.All(x => x.IsApproved)))
			// .AndAlso()
			// .ShouldReturn()
			// .Redirect(redirect => redirect.To<AuthorsController>(c => c.Applications()));
		}
	}
}
