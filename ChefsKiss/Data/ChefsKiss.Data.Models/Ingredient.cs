namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    using static ChefsKiss.Common.DataConstants;

    public class Ingredient : BaseModel<int>
    {
        [Required]
        [MinLength(Ingredients.NameMinLength)]
        [MaxLength(Ingredients.NameMaxLength)]
        public string Name { get; init; }
    }
}
