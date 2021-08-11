namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class IngredientsService : IIngredientsService
    {
        private readonly RecipesDbContext data;

        public IngredientsService(RecipesDbContext data)
        {
            this.data = data;
        }

        public T ById<T>(int id)
        {
            var ingredient = this.data.Ingredients
                .Where(x => x.Id == id)
                .MapTo<T>()
                .First();

            return ingredient;
        }

        public IEnumerable<Ingredient> EnsureAll(IEnumerable<string> ingredientNames)
        {
            var ingredients = this.data.Ingredients
                .Where(x => ingredientNames.Contains(x.Name))
                .ToList();

            foreach (var ingredientName in ingredientNames)
            {
                var ingredient = ingredients.FirstOrDefault(x => x.Name == ingredientName);

                if (ingredient == null)
                {
                    ingredient = new Ingredient { Name = ingredientName };

                    this.data.Ingredients.Add(ingredient);
                }

                ingredients.Add(ingredient);
            }

            return ingredients;
        }
    }
}
