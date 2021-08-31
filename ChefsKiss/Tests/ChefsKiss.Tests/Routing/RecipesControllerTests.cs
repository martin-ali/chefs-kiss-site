namespace ChefsKiss.Tests.Routing
{
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Models.Recipes;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    public class RecipesControllerTests
    {
        [Fact]
        public void CreateGetRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/Recipes/Create")
                    .WithUser())
               .To<RecipesController>(c => c.Create());
        }

        [Fact]
        public void CreatePostRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/Recipes/Create")
                    .WithMethod(HttpMethod.Post)
                    .WithUser())
               .To<RecipesController>(c => c.Create(With.Any<RecipeCreateFormModel>()));
        }


        [Fact]
        public void AllRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Recipes/All/1"))
               .To<RecipesController>(c => c.All());
        }

        [Fact]
        public void SearchGetRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap("/Recipes/Search")
               .To<RecipesController>(c => c.Search());
        }

        [Fact]
        public void SearchPostRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Recipes/Search")
                    .WithMethod(HttpMethod.Post)
                    .WithQuery(With.Any<RecipesQueryModel>()))
               .To<RecipesController>(c => c.Search(With.Any<RecipesQueryModel>()));
        }

        [Fact]
        public void DetailsRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap("/Recipes/Details/1")
               .To<RecipesController>(c => c.Details(1));
        }

        [Fact]
        public void RandomRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap("/Recipes/Random")
               .To<RecipesController>(c => c.Random());
        }

        [Fact]
        public void EditGetRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Recipes/Edit/1")
                    .WithUser())
               .To<RecipesController>(c => c.Edit(1));
        }

        [Fact]
        public void EditPostRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Recipes/Edit/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser()
                    .WithQuery(With.Any<RecipeEditFormModel>()))
               .To<RecipesController>(c => c.Edit(1, With.Any<RecipeEditFormModel>()));
        }

        [Fact]
        public void DeleteGetRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Recipes/Delete/1")
                    .WithUser())
               .To<RecipesController>(c => c.Delete(1));
        }

        [Fact]
        public void DeletePostRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Recipes/Delete/1")
                    .WithMethod(HttpMethod.Post)
                    .WithUser())
               .To<RecipesController>(c => c.DeletePost(1));
        }
    }
}
