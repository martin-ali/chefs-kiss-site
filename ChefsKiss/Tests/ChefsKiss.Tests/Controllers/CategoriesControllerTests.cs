namespace ChefsKiss.Tests.Controllers
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Controllers;
    using ChefsKiss.Web.Models.Categories;

    using MyTested.AspNetCore.Mvc;

    using Xunit;

    using static ChefsKiss.Tests.Data.Items;

    public class CategoriesControllerTests
    {
        [Fact]
        public void DetailsShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<CategoriesController>
            .Instance()
            .WithUser()
            .WithData(Models<Category>(10))
            .Calling(c => c.Details(1))
            .ShouldReturn()
            .View(v => v.WithModelOfType<CategoryDetailsViewModel>());
        }

        [Fact]
        public void ExploreShouldReturnCorrectViewWithCorrectModel()
        {
            MyController<CategoriesController>
            .Instance()
            .WithUser()
            .WithData(Models<Category>(10))
            .Calling(c => c.Explore())
            .ShouldReturn()
            .View(v => v.WithModelOfType<IEnumerable<CategoryCarouselViewModel>>());
        }
    }
}
