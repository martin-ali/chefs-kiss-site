namespace ChefsKiss.Web.Areas.Recipes.Models.Reviews
{
    using System.ComponentModel.DataAnnotations;

    using static ChefsKiss.Common.DataConstants;

    public class ReviewFormModel
    {
        [Required]
        public int RecipeId { get; init; }

        [Required]
        [MinLength(Reviews.ContentMinLength)]
        [MaxLength(Reviews.ContentMaxLength)]
        public string Content { get; init; }

        [Required]
        [Range(Reviews.RatingMinValue, Reviews.RatingMaxValue)]
        public int Rating { get; init; }
    }
}
