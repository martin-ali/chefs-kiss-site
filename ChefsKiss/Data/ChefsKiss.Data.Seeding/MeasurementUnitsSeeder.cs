namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    public class MeasurementUnitsSeeder : IDataSeeder
    {
        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.MeasurementUnits.Any())
            {
                return;
            }

            var units = new string[] { "mg", "kg", "tsp", "tcp" };

            foreach (var unit in units)
            {
                var measurementUnit = new MeasurementUnit { Name = unit };

                await dbContext.MeasurementUnits.AddAsync(measurementUnit);
            }
        }
    }
}