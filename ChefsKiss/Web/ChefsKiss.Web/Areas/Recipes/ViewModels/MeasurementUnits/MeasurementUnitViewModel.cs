namespace ChefsKiss.Web.Areas.Recipes.ViewModels.MeasurementUnits
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class MeasurementUnitViewModel : IMapFrom<MeasurementUnit>
    {
        public int Id { get; init; }

        public string Name { get; init; }
    }
}
