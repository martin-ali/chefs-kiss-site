namespace ChefsKiss.Web.Areas.Recipes.Models.Reviews
{
    using System.ComponentModel.DataAnnotations;

    using static ChefsKiss.Common.DataConstants;
    using static ChefsKiss.Common.ErrorMessages;

    public class ReviewFormModel
    {
        [Required]
        public int RecipeId { get; init; }

        [Required]
        [StringLength(Reviews.ContentMaxLength, MinimumLength = Reviews.ContentMinLength, ErrorMessage = LengthBetween)]

        public string Content { get; init; }

        [Required]
        [Range(Reviews.RatingMinValue, Reviews.RatingMaxValue)]
        public int Rating { get; init; }
    }
}
