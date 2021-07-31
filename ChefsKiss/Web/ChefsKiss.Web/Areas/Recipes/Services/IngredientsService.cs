namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;

    public class IngredientsService : IIngredientsService
    {
        private readonly IRepository<Ingredient> ingredientsRepository;

        public IngredientsService(IRepository<Ingredient> ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task<IEnumerable<Ingredient>> EnsureAllAsync(IEnumerable<string> ingredientNames)
        {
            var ingredients = this.ingredientsRepository.All()
                .Where(x => ingredientNames.Contains(x.Name))
                .ToList();

            foreach (var ingredientName in ingredientNames)
            {
                var ingredient = ingredients.FirstOrDefault(x => x.Name == ingredientName);
                if (ingredient == null)
                {
                    var newIngredient = new Ingredient { Name = ingredientName };
                    ingredients.Add(newIngredient);

                    await this.ingredientsRepository.AddAsync(newIngredient);
                }
            }

            return ingredients;
        }
    }
}
