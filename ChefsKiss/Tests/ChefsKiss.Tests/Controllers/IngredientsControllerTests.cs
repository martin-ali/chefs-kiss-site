namespace ChefsKiss.Tests.Controllers
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.Controllers;
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    public class IngredientsControllerTests
    {
        private readonly Ingredient ingredient = new Ingredient { Id = 1 };

        [Fact]
        public void IngredientAddFormShouldReturnCorrectPartialViewWithCorrectModel()
        {
            MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Recipes/Ingredients/IngredientAddForm/1")
                .WithUser())
            .To<IngredientsController>(c => c.IngredientAddForm(1))
            .Which()
            .ShouldReturn()
            .PartialView(v => v.WithModelOfType<IngredientFormModel>());
        }

        [Fact]
        public void IngredientDetailsShouldReturnCorrectViewWithCorrectModel()
        {
            MyMvc
            .Pipeline()
            .ShouldMap("Recipes/Ingredients/Details/1")
            .To<IngredientsController>(c => c.Details(1))
            .Which()
            .ShouldReturn()
            .View(v => v.WithModelOfType<IngredientDetailsViewModel>());
        }
    }
}
