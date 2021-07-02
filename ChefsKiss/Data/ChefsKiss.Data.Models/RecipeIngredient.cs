namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    public class RecipeIngredient : BaseModel<int>
    {
        [Required]
        public int RecipeId { get; init; }

        public Recipe Recipe { get; init; }

        [Required]
        public int IngredientId { get; init; }

        public Ingredient Ingredient { get; init; }

        [Required]
        public double Quantity { get; init; }

        [Required]
        public MeasurementUnit MeasurementUnit { get; set; }
    }
}
