namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static ChefsKiss.Common.DataConstants;

    public class RecipeCreateFormModel
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [MinLength(RecipeContentMinLength)]
        [MaxLength(RecipeContentMaxLength)]
        public string Content { get; init; }

        [Required]
        [FileExtensions(Extensions = AllowedImageExtensions)]
        public IFormFile Image { get; init; }
    }
}
