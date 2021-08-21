namespace ChefsKiss.Web.Models.Recipes
{
    using System.IO;
    using System.Linq;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Infrastructure.Extensions;

    public class RecipeBaseViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; init; }

        public string AuthorId { get; init; }

        public string AuthorFullName { get; init; }

        public string Title { get; init; }

        public int Rating { get; init; }

        public string ImageSrc { get; init; }

        public virtual void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeBaseViewModel>()
                .ForMember(vm => vm.ImageSrc, cfg => cfg.MapFrom(m => Path.Combine(@"\images", $"{m.Image.Name}.{m.Image.Extension}")))
                .ForMember(vm => vm.AuthorFullName, cfg => cfg.MapFrom(m => $"{m.Author.FirstName} {m.Author.LastName}"))
                .ForMember(vm => vm.AuthorId, cfg => cfg.MapFrom(m => m.Author.UserId))
                .ForMember(vm => vm.Rating, cfg => cfg.MapFrom(m => (int)m.Reviews.Select(x => x.Rating).AverageOrDefault())) // NOTE: .Average() does not like empty collections
                .IncludeAllDerived();
        }
    }
}
