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

        public Task<int> CreateAsync(RecipeCreateFormModel input, string authorId)
        {
            throw new System.NotImplementedException();
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

        public IEnumerable<T> GetByUserId<T>(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
