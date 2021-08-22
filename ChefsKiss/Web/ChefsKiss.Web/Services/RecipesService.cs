namespace ChefsKiss.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ChefsKiss.Common;
    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Models.Ingredients;

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
            var author = this.data.Authors
                .Where(x => x.UserId == userId)
                .First();

            var ingredientsEntities = this.ingredientsService.EnsureAll(ingredients.Select(i => i.Name));

            var recipe = new Recipe
            {
                Title = title,
                Content = content,
                Author = author,
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

        public IEnumerable<T> Popular<T>(int count)
        {
            var recipes = this.data.Recipes
                .OrderByDescending(x => x.Reviews.Average(y => y.Rating))
                .ThenBy(x => x.CreatedOn)
                .Take(count)
                .MapTo<T>()
                .ToList();

            return recipes;
        }

        public IEnumerable<T> PagedAll<T>(int page, int itemsPerPage)
        {
            var itemsToSkip = page * itemsPerPage;
            var recipes = this.PagedWhere<T>(page, itemsPerPage, x => true);

            return recipes;
        }

        public IEnumerable<T> PagedByIngredientId<T>(int page, int itemsPerPage, int ingredientId)
        {
            var itemsToSkip = page * itemsPerPage;
            var recipes = this.PagedWhere<T>(page, itemsPerPage, r => r.RecipeIngredients.Any(i => i.IngredientId == ingredientId));

            return recipes;
        }

        public IEnumerable<T> PagedByCategoryId<T>(int page, int itemsPerPage, int categoryId)
        {
            var itemsToSkip = page * itemsPerPage;
            var recipes = this.PagedWhere<T>(page, itemsPerPage, r => r.CategoryId == categoryId);

            return recipes;
        }

        public IEnumerable<T> PagedBySearchQuery<T>(int page, int itemsPerPage, string searchTerm, int categoryId, RecipesSortBy sortBy)
        {
            var termLowerCase = searchTerm.ToLower();
            var itemsToSkip = page * itemsPerPage;
            var recipesQuery = this.data.Recipes
                .Where(r => r.Title.ToLower().Contains(termLowerCase));

            if (categoryId != 0)
            {
                recipesQuery = recipesQuery.Where(r => r.CategoryId == categoryId);
            }

            recipesQuery = sortBy switch
            {
                RecipesSortBy.Rating => recipesQuery.OrderByDescending(r => r.Reviews.Average(x => x.Rating)),
                RecipesSortBy.Popularity => recipesQuery.OrderByDescending(r => r.Reviews.Count()),
                RecipesSortBy.Newest or _ => recipesQuery.OrderByDescending(r => r.CreatedOn),
            };

            var recipes = recipesQuery
                .Skip(itemsToSkip)
                .Take(itemsPerPage)
                .MapTo<T>()
                .ToList();

            return recipes;
        }

        public IEnumerable<T> PagedWhere<T>(int page, int itemsPerPage, Expression<Func<Recipe, bool>> predicate)
        {
            var itemsToSkip = page * itemsPerPage;
            var recipes = this.data.Recipes
                .Where(predicate)
                .OrderBy(x => x.CreatedOn)
                .Skip(itemsToSkip)
                .Take(itemsPerPage)
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
                .Where(x => x.Author.UserId == id)
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
            var oldRecipeIngredients = this.data.RecipeIngredients
                .Where(x => x.RecipeId == recipe.Id);
            this.recipeIngredientsService.DeleteAll(oldRecipeIngredients);

            var ingredientNames = ingredients.Select(i => i.Name);
            var ingredientEntities = this.ingredientsService.EnsureAll(ingredientNames);
            var recipeIngredients = this.recipeIngredientsService.Create(ingredientEntities, ingredients, recipe);

            // Re-fill all necessary members
            recipe.Title = title;
            recipe.Content = content;
            recipe.RecipeIngredients = recipeIngredients;

            if (image != null)
            {
                var imageEntity = await this.imagesService.CreateImageAsync(image);
                recipe.Image = imageEntity;

                // Delete old image because it is no longer used
                this.imagesService.Delete(recipe.ImageId);
            }

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
