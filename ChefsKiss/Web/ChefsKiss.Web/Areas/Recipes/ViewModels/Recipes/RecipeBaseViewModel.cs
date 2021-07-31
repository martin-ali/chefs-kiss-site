namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.IO;
    using System.Linq;

    using AutoMapper;

    using ChefsKiss.Common.Extensions;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class RecipeBaseViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; init; }

        public string AuthorId { get; init; }

        public string AuthorUsername { get; init; }

        public string Title { get; init; }

        public int Rating { get; init; }

        public string ImageUrl { get; init; }

        public virtual void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Recipe, RecipeBaseViewModel>()
                .ForMember(vm => vm.ImageUrl, cfg => cfg.MapFrom(m => Path.Combine(@"\images", $"{m.Image.Name}.{m.Image.Extension}")))
                .ForMember(vm => vm.Rating, cfg => cfg.MapFrom(m => (int)m.Reviews.Select(x => x.Rating).AverageOrDefault())); // NOTE: .Average() does not like empty collections
        }
    }
}
