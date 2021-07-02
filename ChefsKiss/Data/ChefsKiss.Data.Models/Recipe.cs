namespace ChefsKiss.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    public class Recipe : BaseModel<int>
    {
        [Required]
        public string AuthorId { get; init; }

        public ApplicationUser Author { get; init; }

        [Required]
        [MaxLength(10000)]
        [MinLength(1000)]
        public string Content { get; init; }

        public IEnumerable<RecipeIngredient> RecipeIngredients { get; init; } = new List<RecipeIngredient>();
    }
}
