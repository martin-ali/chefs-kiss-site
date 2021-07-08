namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    using static ChefsKiss.Common.GlobalConstants;

    public class Review : BaseModel<int>
    {
        // [Required]
        public string AuthorId { get; init; }

        public ApplicationUser Author { get; init; }

        // [Required]
        public int RecipeId { get; init; }

        public Recipe Recipe { get; init; }

        [Required]
        [MaxLength(500)]
        public string Content { get; init; }

        [Required]
        [Range(RatingMinValue, RatingMaxValue)]
        public int Rating { get; init; }
    }
}
