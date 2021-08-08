namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    public class MeasurementUnitsSeeder : IDataSeeder
    {

        private static readonly string[] units = new[] {
            "mg",
            "kg",
            "tsp",
            "tcp",
            "whole",
        };

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.MeasurementUnits.Any())
            {
                return;
            }

            foreach (var unit in units)
            {
                var measurementUnit = new MeasurementUnit { Name = unit };

                await dbContext.MeasurementUnits.AddAsync(measurementUnit);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
