namespace ChefsKiss.Data.Models
{
    using ChefsKiss.Data.Common.Models;

    public class RecipeIngredient : BaseModel<int>
    {
        public int RecipeId { get; init; }

        public Recipe Recipe { get; init; }

        public int IngredientId { get; init; }

        public Ingredient Ingredient { get; init; }

        public double Quantity { get; init; }

        public MeasurementUnit MeasurementUnit { get; set; }
    }
}
