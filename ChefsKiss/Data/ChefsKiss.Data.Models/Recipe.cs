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

        public Writer Author { get; init; }

        [Required]
        [MinLength(Recipes.TitleMinLength)]
        [MaxLength(Recipes.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(Recipes.ContentMinLength)]
        [MaxLength(Recipes.ContentMaxLength)]
        public string Content { get; set; }

        // [Required]
        public int ImageId { get; set; }

        public Image Image { get; set; }

        public IEnumerable<Review> Reviews { get; init; } = new List<Review>();

        public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
