namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;

    public class IngredientsService : IIngredientsService
    {
        private readonly RecipesDbContext data;

        public IngredientsService(RecipesDbContext data)
        {
            this.data = data;
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
                    var newIngredient = new Ingredient { Name = ingredientName };
                    ingredients.Add(newIngredient);

                    this.data.Ingredients.Add(newIngredient);
                }
            }

            return ingredients;
        }
    }
}
