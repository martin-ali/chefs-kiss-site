namespace ChefsKiss.Web.Areas.Recipes.Models.Recipes
{
    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    using static ChefsKiss.Common.WebConstants;

    public class RecipeListModel : RecipeBaseViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Summary { get; init; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeListModel>()
                .ForMember(vm => vm.Summary, cfg => cfg.MapFrom(m => m.Content.Substring(0, RecipeSummaryLength)));
        }
    }
}
