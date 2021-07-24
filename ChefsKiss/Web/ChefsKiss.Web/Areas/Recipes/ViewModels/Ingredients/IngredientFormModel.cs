namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients
{
    using System.ComponentModel.DataAnnotations;

    using static ChefsKiss.Common.DataConstants;

    public class IngredientFormModel
    {
        [Required]
        public int Index { get; init; }

        [Required]
        [MinLength(IngredientNameMinLength)]
        [MaxLength(IngredientNameMaxLength)]
        public string Name { get; init; }

        [Required]
        [Range(RecipeIngredientMinQuantity, RecipeIngredientMaxQuantity)]
        public double Quantity { get; init; }

        [Required]
        public int MeasurementUnitId { get; init; }
    }
}
