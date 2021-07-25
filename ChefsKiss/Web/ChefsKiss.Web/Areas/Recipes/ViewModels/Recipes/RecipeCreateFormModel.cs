namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Common.Attributes;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients;

    using Microsoft.AspNetCore.Http;

    using static ChefsKiss.Common.DataConstants;
    using static ChefsKiss.Common.ErrorMessages;

    public class RecipeCreateFormModel
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
    }
}
