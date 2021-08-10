namespace ChefsKiss.Web.Areas.Recipes.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;
    using ChefsKiss.Web.Infrastructure.Attributes;

    using Microsoft.AspNetCore.Http;

    using static ChefsKiss.Common.DataConstants;
    using static ChefsKiss.Common.ErrorMessages;

    public class RecipeFormModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string AuthorId { get; init; }

        [Required]
        [MinLength(Recipes.TitleMinLength)]
        [MaxLength(Recipes.TitleMaxLength)]
        public string Title { get; init; }

        [Required]
        [MinLength(Recipes.ContentMinLength)]
        [MaxLength(Recipes.ContentMaxLength)]
        public string Content { get; init; }

        [Required]
        [ImagesOnly]
        [MaxFileSize(Images.MaxSizeBytes)]
        public IFormFile Image { get; init; }

        [Required]
        [NotEmptyCollection(ErrorMessage = NoIngredients)]
        public IEnumerable<IngredientFormModel> Ingredients { get; init; } = new List<IngredientFormModel>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<Recipe, RecipeFormModel>()
            .ForMember(vm => vm.Ingredients, cfg => cfg.MapFrom(m => m.RecipeIngredients))
            .ForMember(vm => vm.Image, cfg => cfg.Ignore());
        }
    }
}
