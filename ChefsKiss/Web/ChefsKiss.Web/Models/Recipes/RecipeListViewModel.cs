namespace ChefsKiss.Web.Models.Recipes
{
    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    using static ChefsKiss.Common.WebConstants;

    public class RecipeListViewModel : RecipeBaseViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Summary { get; init; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeListViewModel>()
                .ForMember(vm => vm.Summary, cfg => cfg.MapFrom(m => m.Content.Substring(0, RecipeSummaryLength)));
        }
    }
}
