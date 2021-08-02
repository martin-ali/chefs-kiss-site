namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    using static ChefsKiss.Common.DataConstants;

    public class Review : BaseModel<int>
    {
        // [Required]
        public string AuthorId { get; init; }

        public ApplicationUser Author { get; init; }

        // [Required]
        public int RecipeId { get; init; }

        public Recipe Recipe { get; init; }

        [Required]
        [MinLength(Reviews.ContentMinLength)]
        [MaxLength(Reviews.ContentMaxLength)]
        public string Content { get; init; }

        [Required]
        [Range(Reviews.RatingMinValue, Reviews.ContentMaxLength)]
        public int Rating { get; init; }
    }
}
