namespace ChefsKiss.Tests.Data
{
    using System.Collections.Generic;

    using System.Linq;

    using ChefsKiss.Data.Common.Models;
    using ChefsKiss.Data.Models;
    using MyTested.AspNetCore.Mvc;

    public class Items
    {
        public static IEnumerable<T> MockModels<T>(int count) where T : new()
        {
            var recipes = Enumerable
                .Range(0, count)
                .Select(x => new T());

            return recipes;
        }

        public static IEnumerable<Author> AuthorsWithUsers(int count)
        {
            var recipes = Enumerable
                .Range(0, count)
                .Select(x => new Author { User = new ApplicationUser() });

            return recipes;
        }

        public static Recipe RecipeWithDefaultData()
        {
            var recipe = new Recipe
            {
                Category = With.Default<Category>(),
                Image = With.Default<Image>(),
                Author = With.Default<Author>(),
            };

            return recipe;
        }

        public static IEnumerable<Recipe> RecipesWithDefaultData(int count)
        {
            var recipes = Enumerable
                .Range(0, count)
                .Select(r => RecipeWithDefaultData());

            return recipes;
        }
    }
}
