namespace ChefsKiss.Web.Areas.Recipes.Models.Reviews
{
    using System.ComponentModel.DataAnnotations;

    using static ChefsKiss.Common.DataConstants;

    public class ReviewFormModel
    {
        [Required]
        public int RecipeId { get; init; }

        [Required]
        [MinLength(ReviewContentMinLength)]
        [MaxLength(ReviewContentMaxLength)]
        public string Content { get; init; }

        [Required]
        [Range(ReviewRatingMinValue, ReviewRatingMaxValue)]
        public int Rating { get; init; }
    }
}
