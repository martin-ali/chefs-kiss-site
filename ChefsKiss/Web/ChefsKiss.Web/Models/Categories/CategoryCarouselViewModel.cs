namespace ChefsKiss.Web.Models.Categories
{
    using System.Linq;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    using static ChefsKiss.Common.WebConstants;

    public class CategoryCarouselViewModel : CategoryDetailsViewModel, IMapFrom<Category>, IHaveCustomMappings
    {
        public int TotalRecipesCount { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryCarouselViewModel>()
            .ForMember(vm => vm.Recipes, cfg => cfg.MapFrom(m => m.Recipes.Take(RecipesPerCategory)))
            .ForMember(vm => vm.TotalRecipesCount, cfg => cfg.MapFrom(m => m.Recipes.Count()));
        }
    }
}
