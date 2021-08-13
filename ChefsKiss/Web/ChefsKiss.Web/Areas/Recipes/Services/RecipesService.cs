namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.Models.Recipes;

    using Microsoft.AspNetCore.Http;

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

        public async Task<int> CreateAsync(string userId, string title, string content, IEnumerable<IngredientServiceModel> ingredients, IFormFile image)
        {
            var imageEntity = await this.imagesService.CreateImageAsync(image);
            var writer = this.data.Writers
                .Where(x => x.UserId == userId)
                .First();

            var ingredientsEntities = this.ingredientsService.EnsureAll(ingredients.Select(i => i.Name));

            var recipe = new Recipe
            {
                Title = title,
                Content = content,
                Writer = writer,
                Image = imageEntity,
            };

            var recipeIngredients = this.recipeIngredientsService.Create(ingredientsEntities, ingredients, recipe);
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
        public T Random<T>()
        {
            var randomRecipe = this.data.Recipes
                .OrderBy(o => Guid.NewGuid())
                .MapTo<T>()
                .First();

            return randomRecipe;
        }

        public async Task EditAsync(int id, string userId, string title, string content, IEnumerable<IngredientServiceModel> ingredients, IFormFile image)
        {
            var recipe = this.data.Recipes.Find(id);

            // Re-generate members
            var imageEntity = await this.imagesService.CreateImageAsync(image);

            var oldRecipeIngredients = this.data.RecipeIngredients
                .Where(x => x.RecipeId == recipe.Id);
            this.recipeIngredientsService.DeleteAll(oldRecipeIngredients);

            var ingredientEntities = this.ingredientsService.EnsureAll(ingredients.Select(i => i.Name));
            var recipeIngredients = this.recipeIngredientsService.Create(ingredientEntities, ingredients, recipe);

            // Re-fill all necessary members
            recipe.Title = title;
            recipe.Content = content;
            recipe.Image = imageEntity;
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
