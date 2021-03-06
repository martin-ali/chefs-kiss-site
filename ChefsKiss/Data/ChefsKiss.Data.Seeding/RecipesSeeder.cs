namespace ChefsKiss.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.IO;

    using Microsoft.AspNetCore.Hosting;

    using static ChefsKiss.Common.DataConstants;

    public class RecipesSeeder : IDataSeeder
    {
        private const string ImagesSeedingRelativeDirectory = @"Data\ChefsKiss.Data.Seeding\Images";
        private const int RecipesCount = 98;
        private const string RecipeContent = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Na";

        public async Task SeedAsync(RecipesDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Recipes.Any())
            {
                return;
            }

            var random = new Random();
            var authors = dbContext.Authors.Where(x => x.IsApproved).ToList();
            var categories = dbContext.Categories.ToList();
            var images = await GetImages(serviceProvider);

            for (int i = 0; i < RecipesCount; i++)
            {
                var recipeIngredients = CreateRandomRecipeIngredients(dbContext, random);
                var author = authors[random.Next(0, authors.Count)];
                var category = categories[random.Next(0, categories.Count)];
                var image = images[i % images.Count];

                var recipe = new Recipe
                {
                    Author = author,
                    Title = $"Recipe with title {i}",
                    Content = $"{i} - {RecipeContent}",
                    Image = image,
                    Category = category,
                    RecipeIngredients = recipeIngredients,
                };

                dbContext.Recipes.Add(recipe);
            }

            await dbContext.SaveChangesAsync();
        }
        private static IEnumerable<RecipeIngredient> CreateRandomRecipeIngredients(RecipesDbContext dbContext, Random random)
        {
            var measurementUnits = dbContext.MeasurementUnits.ToList();
            var ingredientsRandomized = dbContext.Ingredients
                .OrderBy(x => Guid.NewGuid())
                .ToList();
            var recipeIngredientsCount = random.Next(1, Math.Min(10, ingredientsRandomized.Count));
            var recipeIngredients = new List<RecipeIngredient>();

            for (int i = 0; i < recipeIngredientsCount; i++)
            {
                var randomIngredient = ingredientsRandomized[i];
                var randomMeasurementUnit = measurementUnits[random.Next(0, measurementUnits.Count)];
                var randomQuantity = random.Next((int)RecipeIngredients.MinQuantity, (int)RecipeIngredients.MaxQuantity);

                var recipeIngredient = new RecipeIngredient
                {
                    Ingredient = randomIngredient,
                    MeasurementUnit = randomMeasurementUnit,
                    Quantity = randomQuantity,
                };

                recipeIngredients.Add(recipeIngredient);
            }

            return recipeIngredients;
        }

        private static async Task<IList<Image>> GetImages(IServiceProvider serviceProvider)
        {
            // Get all images for seeding
            var environment = (IWebHostEnvironment)serviceProvider.GetService(typeof(IWebHostEnvironment));
            var root = Path.GetFullPath(Path.Combine(environment.ContentRootPath, @"..\.."));
            var imagesDirectory = Path.Combine(root, ImagesSeedingRelativeDirectory);
            var filePaths = Directory.GetFiles(imagesDirectory);

            var fileWriter = (IImageOperator)serviceProvider.GetService(typeof(IImageOperator));
            var filesCount = filePaths.Length;

            var images = new List<Image>();

            for (int i = 0; i < filesCount; i++)
            {
                var seedPath = filePaths[i];
                var extension = Path
                    .GetExtension(seedPath)
                    .TrimStart('.');

                var image = new Image
                {
                    Name = Guid.NewGuid().ToString(),
                    Extension = extension,
                };

                images.Add(image);

                var bytes = await File.ReadAllBytesAsync(seedPath);
                await fileWriter.WriteAsync(bytes, image.Name, extension);
            }

            return images;
        }
    }
}
