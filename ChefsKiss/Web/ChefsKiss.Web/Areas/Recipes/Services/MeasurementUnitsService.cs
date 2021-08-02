namespace ChefsKiss.Web.Areas.Recipes.Services
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

        public IEnumerable<T> GetAll<T>()
        {
            var units = this.data.MeasurementUnits
                .To<T>();

            return units;
        }
    }
}
