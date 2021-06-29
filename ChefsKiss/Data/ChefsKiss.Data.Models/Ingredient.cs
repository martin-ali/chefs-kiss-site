namespace ChefsKiss.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Data.Common.Models;

    public class Ingredient : BaseModel<int>
    {
        public int RecipeId { get; init; }

        public Recipe Recipe { get; init; }

        public string Name { get; init; }

        public double Quantity { get; init; }

        public MeasurementUnit MeasurementUnit { get; set; }
    }
}
