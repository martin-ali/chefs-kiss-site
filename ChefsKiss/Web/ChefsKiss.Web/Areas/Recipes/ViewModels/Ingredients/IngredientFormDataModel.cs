namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Web.Areas.Recipes.ViewModels.MeasurementUnits;

    public class IngredientFormDataModel
    {
        [Required]
        public int Index { get; init; }

        [Required]
        public IEnumerable<MeasurementUnitViewModel> MeasurementUnitOptions { get; init; }
    }
}
