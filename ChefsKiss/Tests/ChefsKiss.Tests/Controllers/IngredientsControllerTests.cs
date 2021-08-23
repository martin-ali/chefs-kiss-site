namespace ChefsKiss.Tests.Controllers
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Models.Ingredients;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    using static ChefsKiss.Tests.Data.Items;

    public class IngredientsControllerTests
    {
        private readonly Ingredient ingredient = new Ingredient { Id = 1 };

        [Fact]
        public void AddFormShouldReturnCorrectPartialViewWithCorrectModel()
        {
            MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Ingredients/IngredientAddForm/1")
                .WithUser())
            .To<IngredientsController>(c => c.IngredientAddForm(1))
            .Which()
            .ShouldReturn()
            .PartialView(v => v.WithModelOfType<IngredientFormModel>());
        }

        [Fact]
        public void DetailsShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<IngredientsController>
            .Instance()
            .WithData(MockModels<Ingredient>(10))
            .Calling(c => c.Details(1))
            .ShouldReturn()
            .View(v => v.WithModelOfType<IngredientDetailsViewModel>());
        }
    }
}
