namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class MeasurementUnitsService : IMeasurementUnitsService
    {
        private readonly IRepository<MeasurementUnit> measurementUnitsRepository;

        public MeasurementUnitsService(
            IRepository<MeasurementUnit> measurementUnitsRepository)
        {
            this.measurementUnitsRepository = measurementUnitsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var units = this.measurementUnitsRepository.All()
                .To<T>();

            return units;
        }
    }
}
