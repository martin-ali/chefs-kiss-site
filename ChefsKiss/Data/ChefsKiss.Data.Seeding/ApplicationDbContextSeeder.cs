namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Data;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class ApplicationDbContextSeeder : IDataSeeder
    {
        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(ApplicationDbContextSeeder));

            var seeders = new List<IDataSeeder>
            {
                new MeasurementUnitsSeeder(),
        };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}
