namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    public class Ingredient : BaseModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; init; }
    }
}
