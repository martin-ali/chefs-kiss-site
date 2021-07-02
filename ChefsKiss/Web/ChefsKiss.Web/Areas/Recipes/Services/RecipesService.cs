namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IRepository<Recipe> recipesRepository;

        public RecipesService(IRepository<Recipe> recipesRepository)
        {
            this.recipesRepository = recipesRepository;
        }

        public async Task<int> CreateAsync(RecipeCreateFormModel input, string authorId)
        {
            var recipe = new Recipe
            {
                Name = input.Name,
                Content = input.Content,
                AuthorId = authorId,
            };

            await this.recipesRepository.AddAsync(recipe);

            await this.recipesRepository.SaveChangesAsync();

            return recipe.Id;
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
