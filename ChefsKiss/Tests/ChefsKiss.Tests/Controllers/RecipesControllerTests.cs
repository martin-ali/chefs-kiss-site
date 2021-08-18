namespace ChefsKiss.Tests.Controllers
{
	using ChefsKiss.Data.Models;
	using ChefsKiss.Tests.Data;
	using ChefsKiss.Web.Areas.Recipes.Controllers;
	using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

	using MyTested.AspNetCore.Mvc;

	using Xunit;

	using static ChefsKiss.Common.WebConstants;

	public class RecipesControllerTests
	{
		[Fact]
		public void Test()
		{
			MyController<RecipesController>
			.Instance()
			.WithUser()
			.WithData(Items.TenItems<Recipe>())
			.Calling(c => c.Details(1))
			.ShouldReturn()
			.View(v => v.WithModelOfType<RecipeDetailsViewModel>());
		}
	}
}
