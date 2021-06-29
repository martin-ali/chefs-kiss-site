namespace ChefsKiss.Data.Models
{
    using ChefsKiss.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public string AuthorId { get; init; }

        public ApplicationUser Author { get; init; }
    }
}
