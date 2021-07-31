namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    using static ChefsKiss.Common.DataConstants;

    public class RecipeIngredient : BaseModel<int>
    {
        // [Required]
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        // [Required]
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }

        // [Required]
        public int MeasurementUnitId { get; set; }

        public MeasurementUnit MeasurementUnit { get; set; }

        [Required]
        [Range(RecipeIngredientMinQuantity, RecipeIngredientMaxQuantity)]
        public double Quantity { get; set; }
    }
}
