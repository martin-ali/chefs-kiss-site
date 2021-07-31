namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using ChefsKiss.Common.Attributes;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients;

    using Microsoft.AspNetCore.Http;

    using static ChefsKiss.Common.DataConstants;
    using static ChefsKiss.Common.ErrorMessages;

    public class RecipeFormModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        [Required]
        [MinLength(RecipeTitleMinLength)]
        [MaxLength(RecipeTitleMaxLength)]
        public string Title { get; init; }

        [Required]
        [MinLength(RecipeContentMinLength)]
        [MaxLength(RecipeContentMaxLength)]
        public string Content { get; init; }

        [Required]
        [ImagesOnly]
        [MaxFileSize(ImageMaxSizeBytes)]
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
