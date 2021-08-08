namespace ChefsKiss.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    using static ChefsKiss.Common.DataConstants;

    public class Writer : BaseModel<int>
    {
        public string UserId { get; init; }

        public ApplicationUser User { get; init; }

        [Required]
        [MinLength(Writers.NameMinLength)]
        [MaxLength(Writers.NameMaxLength)]
        public string FirstName { get; init; }

        [Required]
        [MinLength(Writers.NameMinLength)]
        [MaxLength(Writers.NameMaxLength)]
        public string LastName { get; init; }

        public IEnumerable<Recipe> Recipes { get; init; } = new List<Recipe>();
    }
}
