namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IRepository<Recipe> recipesRepository;
        private readonly IRepository<Ingredient> ingredientsRepository;
        private readonly IRepository<RecipeIngredient> recipeIngredientsRepository;
        private readonly IRecipeIngredientsService recipeIngredientsService;
        private readonly IImagesService imagesService;

        public RecipesService(
            IRepository<Recipe> recipesRepository,
            IRepository<Ingredient> ingredientsRepository,
            IRepository<RecipeIngredient> recipeIngredientsRepository,
            IRecipeIngredientsService recipeIngredientsService,
            IImagesService imagesService)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipeIngredientsRepository = recipeIngredientsRepository;
            this.recipeIngredientsService = recipeIngredientsService;
            this.imagesService = imagesService;
        }

        public async Task<int> CreateAsync(RecipeFormModel input, string authorId)
        {
            var image = await this.imagesService.CreateImageAsync(input.Image);

            var ingredients = await this.EnsureIngredientsAsync(input.Ingredients.Select(i => i.Name));

            var recipe = new Recipe
            {
                Title = input.Title,
                Content = input.Content,
                AuthorId = authorId,
                Image = image,
            };

            var recipeIngredients = await this.recipeIngredientsService.CreateAsync(ingredients, input.Ingredients, recipe);
            recipe.RecipeIngredients = recipeIngredients;

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();

            return recipe.Id;
        }

        private async Task<IEnumerable<Ingredient>> EnsureIngredientsAsync(IEnumerable<string> ingredientNames)
        {
            var ingredients = this.ingredientsRepository.All()
                .Where(i => ingredientNames.Contains(i.Name))
                .ToList();

            foreach (var ingredientName in ingredientNames)
            {
                var ingredient = ingredients.FirstOrDefault(i => i.Name == ingredientName);
                if (ingredient == null)
                {
                    var newIngredient = new Ingredient { Name = ingredientName };
                    ingredients.Add(newIngredient);

                    await this.ingredientsRepository.AddAsync(newIngredient);
                }
            }

            return ingredients;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var recipes = this.recipesRepository
                .All()
                .To<T>()
                .ToList();

            return recipes;
        }

        public IEnumerable<T> GetByCategory<T>(int category)
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            var recipe = this.recipesRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .First();

            return recipe;
        }

        public IEnumerable<T> GetByAuthorId<T>(string authorId)
        {
            var recipes = this.recipesRepository
                .All()
                .Where(x => x.AuthorId == authorId)
                .To<T>()
                .ToList();

            return recipes;
        }

        //FIXME: Should work even when recipes are deleted
        public T GetRandom<T>()
        {
            var randomRecipe = this.recipesRepository
                .All()
                .OrderBy(o => Guid.NewGuid())
                .To<T>()
                .First();

            return randomRecipe;
        }

        public async Task EditAsync(RecipeFormModel input, int recipeId)
        {
            var recipe = this.recipesRepository
                .All()
                .First(x => x.Id == recipeId);

            // Re-generate members
            var image = await this.imagesService.CreateImageAsync(input.Image);

            var oldRecipeIngredients = this.recipeIngredientsRepository
                .All()
                .Where(x => x.RecipeId == recipe.Id);
            await this.recipeIngredientsService.DeleteAllAsync(oldRecipeIngredients);

            var ingredients = await this.EnsureIngredientsAsync(input.Ingredients.Select(i => i.Name));
            var recipeIngredients = await this.recipeIngredientsService.CreateAsync(ingredients, input.Ingredients, recipe);

            // Re-fill all necessary members
            recipe.Title = input.Title;
            recipe.Content = input.Content;
            recipe.Image = image;
            recipe.RecipeIngredients = recipeIngredients;

            // Delete old image because it is no longer used
            await this.imagesService.DeleteAsync(recipe.ImageId);

            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
