namespace ChefsKiss.Web.Areas.Home.Controllers
{
	using System.Diagnostics;
	using ChefsKiss.Web.Areas.Recipes.Models.Recipes;
	using ChefsKiss.Web.Areas.Recipes.Services;
	using ChefsKiss.Web.Models;

	using Microsoft.AspNetCore.Mvc;

	public class HomeController : Controller
	{
		private readonly IRecipesService recipes;

		public HomeController(
			IRecipesService recipes)
		{
			this.recipes = recipes;
		}

		[HttpGet("/")]
		[HttpGet("/Index")]
		public IActionResult Index()
		{
			var recipes = this.recipes
			.Popular<RecipeListViewModel>(5);

			return this.View(recipes);
		}

		[HttpGet]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
		}
	}
}
