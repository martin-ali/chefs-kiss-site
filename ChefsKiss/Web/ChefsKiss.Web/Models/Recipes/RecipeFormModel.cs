namespace ChefsKiss.Web.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Infrastructure.Attributes;
    using ChefsKiss.Web.Models.Ingredients;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static ChefsKiss.Common.DataConstants;
    using static ChefsKiss.Common.ErrorMessages;

    public abstract class RecipeFormModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string AuthorId { get; init; }

        [Required]
        [StringLength(Recipes.TitleMaxLength, MinimumLength = Recipes.TitleMinLength, ErrorMessage = LengthBetween)]
        public string Title { get; init; }

        [Required]
        [StringLength(Recipes.ContentMaxLength, MinimumLength = Recipes.ContentMinLength, ErrorMessage = LengthBetween)]
        public string Content { get; init; }

        [ImagesOnly]
        [MaxFileSize(Images.MaxSizeBytes)]
        public virtual IFormFile Image { get; init; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        [Required]
        [NotEmptyCollection(ErrorMessage = NoIngredients)]
        public IEnumerable<IngredientFormModel> Ingredients { get; init; } = new List<IngredientFormModel>();

        public virtual void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeFormModel>()
            .ForMember(vm => vm.Ingredients, cfg => cfg.MapFrom(m => m.RecipeIngredients))
            .ForMember(vm => vm.Image, cfg => cfg.Ignore())
            .IncludeAllDerived();
        }
    }
}
