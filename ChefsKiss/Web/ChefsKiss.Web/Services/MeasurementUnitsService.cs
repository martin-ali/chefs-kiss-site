namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;

    using ChefsKiss.Data;
    using ChefsKiss.Services.Mapping;

    public class MeasurementUnitsService : IMeasurementUnitsService
    {
        private readonly RecipesDbContext data;

        public MeasurementUnitsService(RecipesDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<T> All<T>()
        {
            var units = this.data.MeasurementUnits
                .MapTo<T>();

            return units;
        }
    }
}
