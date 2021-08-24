namespace ChefsKiss.Data.Models
{
    using ChefsKiss.Data.Common.Models;

    public class Favorite : BaseModel<int>
    {
        public string UserId { get; init; }

        public ApplicationUser User { get; init; }

        public int RecipeId { get; init; }

        public Recipe Recipe { get; init; }
    }
}
