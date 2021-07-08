namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Reviews;

    public class RecipeDetailsViewModel : RecipeInListViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Content { get; init; }

        public IEnumerable<ReviewListViewModel> Reviews { get; init; } = new List<ReviewListViewModel>();
    }
}
