namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.IO;
    using System.Linq;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class RecipeInListViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Recipe, RecipeInListViewModel>()
                .Include<Recipe, RecipeDetailsViewModel>()
                .ForMember(vm => vm.ImageUrl, opt => opt.MapFrom(r => Path.Combine(@"\images", $"{r.Image.Name}.{r.Image.Extension}")));

            // FIXME: THIS SHOULD NOT BE HERE
            configuration
                .CreateMap<Recipe, RecipeDetailsViewModel>()
                .ForMember(vm => vm.Reviews, opt => opt.MapFrom(r => r.Reviews.OrderByDescending(c => c.CreatedOn)));
        }
    }
}
