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
        private readonly IImagesService imagesService;

        public RecipesService(
            IRepository<Recipe> recipesRepository,
            IRepository<Ingredient> ingredientsRepository,
            IRepository<RecipeIngredient> recipeIngredientsRepository,
            IImagesService imagesService)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipeIngredientsRepository = recipeIngredientsRepository;
            this.imagesService = imagesService;
        }

        public async Task<int> CreateAsync(RecipeCreateFormModel input, string authorId)
        {
            var image = await this.imagesService.CreateImage(input.Image, authorId);

            var ingredients = await this.EnsureIngredients(input.Ingredients.Select(i => i.Name));

            var recipe = new Recipe
            {
                Name = input.Name,
                Content = input.Content,
                AuthorId = authorId,
                Image = image,
            };

            var recipeIngredients = await this.EnsureRecipeIngredients(ingredients, input.Ingredients, recipe);
            recipe.RecipeIngredients = recipeIngredients;

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();

            return recipe.Id;
        }

        private async Task<IEnumerable<RecipeIngredient>> EnsureRecipeIngredients(
            IEnumerable<Ingredient> ingredients,
            IEnumerable<IngredientFormModel> ingredientModels,
            Recipe recipe)
        {
            var recipeIngredients = new List<RecipeIngredient>();

            foreach (var ingredient in ingredients)
            {
                var ingredientData = ingredientModels.FirstOrDefault(im => im.Name == ingredient.Name);

                var newRecipeIngredient = new RecipeIngredient
                {
                    Recipe = recipe,
                    Ingredient = ingredient,
                    MeasurementUnitId = ingredientData.MeasurementUnitId,
                    Quantity = ingredientData.Quantity,
                };

                recipeIngredients.Add(newRecipeIngredient);

                await this.recipeIngredientsRepository.AddAsync(newRecipeIngredient);
            }

            return recipeIngredients;
        }

        private async Task<IEnumerable<Ingredient>> EnsureIngredients(IEnumerable<string> ingredientNames)
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
                .FirstOrDefault();

            return recipe;
        }

        public IEnumerable<T> GetByUserId<T>(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
