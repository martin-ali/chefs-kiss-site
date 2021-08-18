namespace ChefsKiss.Tests.Routing
{
	using System.Collections.Generic;

	using ChefsKiss.Web.Areas.Recipes.Controllers;
	using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;
	using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

	using Microsoft.AspNetCore.Http;

	using MyTested.AspNetCore.Mvc;

	using Xunit;

	public class RecipesControllerTests
	{
		private const string SearchTerm = "Term";

		[Fact]
		public void PagedRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
				.ShouldMap("/Recipes/Recipes/Paged/1")
			   .To<RecipesController>(c => c.Paged(1));
		}

		[Fact]
		public void PagedByIngredientIdRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
				.ShouldMap(r => r
					.WithPath("/Recipes/Recipes/PagedByIngredientId/1")
					.WithQuery("recipeId", "1"))
			   .To<RecipesController>(c => c.PagedByIngredientId(1, 1));
		}

		[Fact]
		public void PagedBySearchTermRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
				.ShouldMap(r => r
					.WithPath("/Recipes/Recipes/PagedBySearchTerm/1")
					.WithQuery("searchTerm", SearchTerm))
			   .To<RecipesController>(c => c.PagedBySearchTerm(1, SearchTerm));
		}

		[Fact]
		public void CreateGetRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
				.ShouldMap(r => r
					.WithPath("/Recipes/Recipes/Create")
					.WithUser())
			   .To<RecipesController>(c => c.Create());
		}

		[Fact]
		public void CreatePostRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
				.ShouldMap(r => r
					.WithPath("/Recipes/Recipes/Create")
					.WithMethod(HttpMethod.Post)
					.WithUser())
			   .To<RecipesController>(c => c.Create(With.Any<RecipeFormModel>()));
		}


		[Fact]
		public void AllRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
			   .ShouldMap(r => r
					.WithPath("/Recipes/Recipes/All/1"))
			   .To<RecipesController>(c => c.All());
		}

		[Fact]
		public void SearchRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
			   .ShouldMap(r => r
					.WithPath("/Recipes/Recipes/Search")
					.WithQuery("searchTerm", SearchTerm))
			   .To<RecipesController>(c => c.Search(SearchTerm));
		}

		[Fact]
		public void DetailsRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
			   .ShouldMap("/Recipes/Recipes/Details/1")
			   .To<RecipesController>(c => c.Details(1));
		}

		[Fact]
		public void RandomRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
			   .ShouldMap("/Recipes/Recipes/Random")
			   .To<RecipesController>(c => c.Random());
		}

		[Fact]
		public void EditRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
			   .ShouldMap(r => r
					.WithPath("/Recipes/Recipes/Edit/1")
					.WithUser())
			   .To<RecipesController>(c => c.Edit(1));
		}

		[Fact]
		public void DeleteGetRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
			   .ShouldMap(r => r
					.WithPath("/Recipes/Recipes/Delete/1")
					.WithUser())
			   .To<RecipesController>(c => c.Delete(1));
		}

		[Fact]
		public void DeletePostRouteShouldBeMapped()
		{
			MyRouting
			   .Configuration()
			   .ShouldMap(r => r
					.WithPath("/Recipes/Recipes/Delete/1")
					.WithMethod(HttpMethod.Post)
					.WithUser())
			   .To<RecipesController>(c => c.DeletePost(1));
		}
	}
}
