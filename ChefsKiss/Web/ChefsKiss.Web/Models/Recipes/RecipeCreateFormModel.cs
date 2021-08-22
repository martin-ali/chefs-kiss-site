namespace ChefsKiss.Web.Models.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Web.Infrastructure.Attributes;

    using Microsoft.AspNetCore.Http;

    using static ChefsKiss.Common.DataConstants;

    public class RecipeCreateFormModel : RecipeFormModel
    {
        [Required]
        [ImagesOnly]
        [MaxFileSize(Images.MaxSizeBytes)]
        public override IFormFile Image { get; init; }
    }
}
