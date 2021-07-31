namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    public class Image : BaseModel<int>
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public string Extension { get; init; }

        public int RecipeId { get; }

        public Recipe Recipe { get; }
    }
}
