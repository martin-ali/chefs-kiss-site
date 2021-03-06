namespace ChefsKiss.Tests.Routing
{
    using ChefsKiss.Web.Controllers;

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
                    .WithPath("/Ingredients/IngredientAddForm/1")
                    .WithUser())
               .To<IngredientsController>(c => c.IngredientAddForm(1));
        }

        [Fact]
        public void DetailsRouteShouldBeMapped()
        {
            MyRouting
               .Configuration()
               .ShouldMap(r => r
                    .WithPath("/Ingredients/Details/1")
                    .WithUser())
               .To<IngredientsController>(c => c.Details(1));
        }
    }
}
