namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly RecipesDbContext data;
        private readonly IRecipeIngredientsService recipeIngredientsService;
        private readonly IIngredientsService ingredientsService;
        private readonly IImagesService imagesService;

        public RecipesService(
            RecipesDbContext data,
            IRecipeIngredientsService recipeIngredientsService,
            IIngredientsService ingredientsService,
            IImagesService imagesService)
        {
            this.data = data;
            this.recipeIngredientsService = recipeIngredientsService;
            this.ingredientsService = ingredientsService;
            this.imagesService = imagesService;
        }

        public async Task<int> CreateAsync(RecipeFormModel input, string userId)
        {
            var image = await this.imagesService.CreateImageAsync(input.Image);
            var writer = this.data.Writers
                .Where(x => x.UserId == userId)
                .First();

            var ingredients = this.ingredientsService.EnsureAll(input.Ingredients.Select(i => i.Name));

            var recipe = new Recipe
            {
                Title = input.Title,
                Content = input.Content,
                Writer = writer,
                Image = image,
            };

            var recipeIngredients = this.recipeIngredientsService.Create(ingredients, input.Ingredients, recipe);
            recipe.RecipeIngredients = recipeIngredients;

            this.data.Recipes.Add(recipe);
            this.data.SaveChanges();

            return recipe.Id;
        }

        public IEnumerable<T> All<T>()
        {
            var recipes = this.data.Recipes
                .MapTo<T>()
                .ToList();

            return recipes;
        }

        public IEnumerable<T> Paged<T>(int page, int itemsPerPage)
        {
            var itemsToSkip = page * itemsPerPage;
            var recipes = this.data.Recipes
                .Skip(itemsToSkip)
                .Take(itemsPerPage)
                .MapTo<T>()
                .ToList();

            return recipes;
        }

        public IEnumerable<T> ByIngredientId<T>(int id)
        {
            var recipes = this.data.Recipes
                .Where(x => x.RecipeIngredients.Any(y => y.IngredientId == id))
                .MapTo<T>()
                .ToList();

            return recipes;
        }

        public T ById<T>(int id)
        {
            var recipe = this.data.Recipes
                .Where(x => x.Id == id)
                .MapTo<T>()
                .First();

            return recipe;
        }

        public IEnumerable<T> ByAuthorId<T>(string id)
        {
            var recipes = this.data.Recipes
                .Where(x => x.Writer.UserId == id)
                .MapTo<T>()
                .ToList();

            return recipes;
        }

        // FIXME: Should work even when recipes are deleted
        public T GetRandom<T>()
        {
            var randomRecipe = this.data.Recipes
                .OrderBy(o => Guid.NewGuid())
                .MapTo<T>()
                .First();

            return randomRecipe;
        }

        public async Task EditAsync(int id, RecipeFormModel input)
        {
            var recipe = this.data.Recipes.Find(id);

            // Re-generate members
            var image = await this.imagesService.CreateImageAsync(input.Image);

            var oldRecipeIngredients = this.data.RecipeIngredients
                .Where(x => x.RecipeId == recipe.Id);
            this.recipeIngredientsService.DeleteAll(oldRecipeIngredients);

            var ingredients = this.ingredientsService.EnsureAll(input.Ingredients.Select(i => i.Name));
            var recipeIngredients = this.recipeIngredientsService.Create(ingredients, input.Ingredients, recipe);

            // Re-fill all necessary members
            recipe.Title = input.Title;
            recipe.Content = input.Content;
            recipe.Image = image;
            recipe.RecipeIngredients = recipeIngredients;

            // Delete old image because it is no longer used
            this.imagesService.Delete(recipe.ImageId);

            this.data.SaveChanges();
        }

        public void Remove(int id)
        {
            var recipe = this.data.Recipes.Find(id);

            this.data.Recipes.Remove(recipe);

            this.data.SaveChanges();
        }
    }
}
