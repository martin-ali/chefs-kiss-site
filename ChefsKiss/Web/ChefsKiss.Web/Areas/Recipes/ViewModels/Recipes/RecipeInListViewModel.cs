namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    using static ChefsKiss.Common.DataConstants;

    public class RecipeInListViewModel : RecipeBaseViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Summary { get; init; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            base.CreateMappings(configuration);

            configuration
                .CreateMap<Recipe, RecipeInListViewModel>()
                .IncludeBase<Recipe, RecipeBaseViewModel>()
                .ForMember(vm => vm.Summary, cfg => cfg.MapFrom(m => $"{m.Content.Substring(0, RecipeSummaryLength - 3)}..."));
        }
    }
}
