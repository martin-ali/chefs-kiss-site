namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        public string AuthorId { get; init; }

        public ApplicationUser Author { get; init; }

        public int RecipeId { get; init; }

        public Recipe Recipe { get; init; }

        public string Content { get; init; }
    }
}
