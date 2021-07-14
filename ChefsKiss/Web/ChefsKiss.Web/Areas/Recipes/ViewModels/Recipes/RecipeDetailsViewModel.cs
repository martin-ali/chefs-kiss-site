namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.Collections.Generic;

    using AutoMapper.Configuration.Annotations;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Reviews;

    public class RecipeDetailsViewModel : RecipeInListViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string AuthorId { get; init; }

        public string AuthorUsername { get; init; }

        public string Content { get; init; }

        public IEnumerable<ReviewListViewModel> Reviews { get; init; } = new List<ReviewListViewModel>();

        [SourceMember(nameof(Recipe.RecipeIngredients))]
        public IEnumerable<IngredientViewModel> Ingredients { get; init; } = new List<IngredientViewModel>();
    }
}
