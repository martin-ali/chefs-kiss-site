namespace ChefsKiss.Web.Models.Recipes
{
    using System.Collections.Generic;
    using System.IO;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Models.Ingredients;

    public class RecipeDeleteModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; init; }

        public string AuthorId { get; init; }

        public string Title { get; init; }

        public string ImageSrc { get; init; }

        public string Content { get; init; }

        public IEnumerable<IngredientViewModel> Ingredients { get; init; } = new List<IngredientViewModel>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeDeleteModel>()
              .ForMember(vm => vm.ImageSrc, cfg => cfg.MapFrom(m => Path.Combine(@"\images", $"{m.Image.Name}.{m.Image.Extension}")))
              .ForMember(vm => vm.Ingredients, cfg => cfg.MapFrom(m => m.RecipeIngredients))
              .ForMember(vm => vm.AuthorId, cfg => cfg.MapFrom(m => m.Author.UserId));
        }
    }
}
