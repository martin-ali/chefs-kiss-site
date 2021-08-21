namespace ChefsKiss.Web.Models.MeasurementUnits
{
    using System.Collections.Generic;

    public class MeasurementUnitsComponentModel
    {
        public int Selected { get; init; }

        public IEnumerable<MeasurementUnitViewModel> Units { get; init; }
    }
}
