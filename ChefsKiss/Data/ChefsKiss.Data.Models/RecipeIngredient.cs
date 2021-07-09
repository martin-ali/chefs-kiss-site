namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    using static ChefsKiss.Common.DataConstants;

    public class RecipeIngredient : BaseModel<int>
    {
        // [Required]
        public int RecipeId { get; init; }

        public Recipe Recipe { get; init; }

        // [Required]
        public int IngredientId { get; init; }

        public Ingredient Ingredient { get; init; }

        // [Required]
        public int MeasurementUnitId { get; set; }

        public MeasurementUnit MeasurementUnit { get; set; }

        [Required]
        [Range(RecipeIngredientMinQuantity, RecipeIngredientMaxQuantity)]
        public double Quantity { get; init; }
    }
}
