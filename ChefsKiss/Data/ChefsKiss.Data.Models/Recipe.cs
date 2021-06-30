namespace ChefsKiss.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    public class Recipe : BaseModel<int>
    {
        public string AuthorId { get; init; }

        public ApplicationUser Author { get; init; }

        public string Content { get; init; }

        public IEnumerable<RecipeIngredient> RecipeIngredients { get; init; } = new List<RecipeIngredient>();
    }
}
