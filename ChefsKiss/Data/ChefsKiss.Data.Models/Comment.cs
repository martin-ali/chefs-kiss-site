namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        public string AuthorId { get; init; }

        public ApplicationUser Author { get; init; }
    }
}
