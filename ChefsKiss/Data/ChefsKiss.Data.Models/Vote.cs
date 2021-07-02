namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        // [Required]
        public string AuthorId { get; init; }

        public ApplicationUser Author { get; init; }

        // [Required]
        public int RecipeId { get; init; }

        public Recipe Recipe { get; init; }

        [Required]
        [Range(1, 5)]
        public float Rating { get; init; }
    }
}
