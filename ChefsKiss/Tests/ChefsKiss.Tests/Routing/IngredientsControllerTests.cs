namespace ChefsKiss.Tests.Routing
{
    using ChefsKiss.Web.Areas.Recipes.Controllers;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    public class IngredientsControllerTests
    {
        [Fact]
        public void IngredientAddFormRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
                .ShouldMap(r => r
                    .WithPath("/Recipes/Ingredients/IngredientAddForm/1")
                    .WithUser())
               .To<IngredientsController>(c => c.IngredientAddForm(1));
        }

        [Fact]
        public void DetailsRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Recipes/Ingredients/Details/1")
                    .WithUser())
               .To<IngredientsController>(c => c.Details(1));
        }
    }
}
