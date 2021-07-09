namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients
{
    using System.ComponentModel.DataAnnotations;

    using static ChefsKiss.Common.DataConstants;

    public class IngredientFormModel
    {
        [Required]
        [MinLength(IngredientNameMinLength)]
        [MaxLength(IngredientNameMaxLength)]
        public string Name { get; init; }

        [Required]
        [Range(RecipeIngredientMinQuantity, RecipeIngredientMaxQuantity)]
        public double Quantity { get; init; }

        [Required]
        [MinLength(MeasurementUnitNameMinLength)]
        [MaxLength(MeasurementUnitNameMaxLength)]
        public string MeasurementUnit { get; init; }
    }
}
