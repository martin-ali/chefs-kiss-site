namespace ChefsKiss.Tests.Controllers
{
	using ChefsKiss.Data.Models;
	using ChefsKiss.Web.Areas.Recipes.Controllers;
	using ChefsKiss.Web.Areas.Recipes.Models.Reviews;

	using FluentAssertions;

	using MyTested.AspNetCore.Mvc;

	using Xunit;

	using static ChefsKiss.Tests.Data.Items;
	using static ChefsKiss.Common.WebConstants;

	public class ReviewsControllerTests
	{
		[Fact]
		public void CreateShouldReturnCorrectViewWithCorrectModel()
		{
			MyMvc
			.Pipeline()
			.ShouldMap("/Recipes/Reviews/Create")
			.To<ReviewsController>(x => x.Create(With.Any<ReviewFormModel>()))
			.Which(controller => controller.WithData(TenItems<Review>()))
			.ShouldReturn()
			.View(view => view
				.WithModelOfType<ReviewDetailsViewModel>()
				.Passing(m => m.Should().BeOfType<ReviewDetailsViewModel>()));
		}

		[Fact]
		public void DetailsShouldReturnCorrectViewWithCorrectModel()
		{
			MyMvc
			.Pipeline()
			.ShouldMap("/Recipes/Reviews/Details/1")
			.To<ReviewsController>(x => x.Details(1))
			.Which(controller => controller.WithData(TenItems<Review>()))
			.ShouldReturn()
			.View(view => view.WithModelOfType<ReviewDetailsViewModel>());
		}

		[Fact]
		public void DeleteShouldReturnCorrectViewWithCorrectModel()
		{
			// MyController<ReviewsController>
			// .Instance()
			// .WithData(x => x.WithSet<Review>())
			// .WithUser()
			// .Calling(c => c.Delete(1, 1))
			// .ShouldReturn()
			// .Redirect(redirect => redirect.To<RecipesController>(c => c.Details(1)));

			// MyMvc
			// .Pipeline()
			// .ShouldMap(r => r
			//     .WithPath("/Recipes/Reviews/Del~ete/1")
			//     .WithQuery("recipeId", "1")
			//     .WithUser(new[] { AdministratorRoleName }))
			// .To<ReviewsController>(x => x.Delete(1, 1))
			// .Which(controller => controller.WithData(TenItems<Review>()))
			// .ShouldReturn()
			// .Redirect(redirect => redirect.To<RecipesController>(c => c.Details(1)));
		}
	}
}
