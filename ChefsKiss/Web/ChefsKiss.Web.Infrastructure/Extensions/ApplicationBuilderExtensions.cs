namespace ChefsKiss.Web.Infrastructure.Extensions
{
    using System.IO;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Seeding;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static ChefsKiss.Common.WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ReseedDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var env = serviceScope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var images = Path.Combine(env.WebRootPath, ImagesDirectory);
            if (Directory.Exists(images))
            {
                Directory.Delete(images, recursive: true);
            }

            var dbContext = serviceScope.ServiceProvider.GetRequiredService<RecipesDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();

            new ApplicationDbContextSeeder()
                .SeedAsync(dbContext, serviceScope.ServiceProvider)
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
