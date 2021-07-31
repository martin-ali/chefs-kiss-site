namespace ChefsKiss.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    using static ChefsKiss.Common.DataConstants;

    public class Recipe : BaseModel<int>
    {
        // [Required]
        public string AuthorId { get; init; }

        public ApplicationUser Author { get; init; }

        [Required]
        [MinLength(RecipeTitleMinLength)]
        [MaxLength(RecipeTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(RecipeContentMinLength)]
        [MaxLength(RecipeContentMaxLength)]
        public string Content { get; set; }

        // [Required]
        public int ImageId { get; set; }

        public Image Image { get; set; }

        public IEnumerable<Review> Reviews { get; init; } = new List<Review>();

        public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
