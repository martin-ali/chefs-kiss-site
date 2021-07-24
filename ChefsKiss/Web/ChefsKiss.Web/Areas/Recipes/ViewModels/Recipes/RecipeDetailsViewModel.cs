namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Reviews;

    public class RecipeDetailsViewModel : RecipeBaseViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string AuthorId { get; init; }

        public string AuthorUsername { get; init; }

        public string Content { get; init; }

        public IEnumerable<ReviewListViewModel> Reviews { get; init; } = new List<ReviewListViewModel>();

        public IEnumerable<IngredientViewModel> Ingredients { get; init; } = new List<IngredientViewModel>();

        public override void CreateMappings(IProfileExpression configuration)
        {
            base.CreateMappings(configuration);

            configuration
                .CreateMap<Recipe, RecipeDetailsViewModel>()
                .IncludeBase<Recipe, RecipeBaseViewModel>()
                .ForMember(vm => vm.Reviews, opt => opt.MapFrom(r => r.Reviews.OrderByDescending(c => c.CreatedOn)))
                .ForMember(vm => vm.Ingredients, opt => opt.MapFrom(r => r.RecipeIngredients));
        }

    }
}
