namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.ViewModels;

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

        public IEnumerable<Recipe> GetAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetByCategory<T>(int category)
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            throw new System.NotImplementedException();
        }

        // public T GetById<T>(int id)
        // {
        //     var recipe = this.recipesRepository
        //         .All()
        //         .FirstOrDefault(x => x.Id == id);

        //     return recipe;
        // }

        public IEnumerable<T> GetByUserId<T>(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
