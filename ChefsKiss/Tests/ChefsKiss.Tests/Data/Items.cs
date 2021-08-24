namespace ChefsKiss.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Data.Common.Models;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Models.Ingredients;
    using ChefsKiss.Web.Models.Recipes;

    using MyTested.AspNetCore.Mvc;

    using static ChefsKiss.Common.DataConstants;

    public class Items
    {
        public static IEnumerable<T> Models<T>(int count) where T : new()
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

        public static Recipe RecipeWithDefaultDataAndCategoryId(int categoryId)
        {
            var recipe = new Recipe
            {
                Title = "recipe",
                Content = new string('*', Recipes.ContentMinLength),
                CategoryId = categoryId,
                Image = With.Default<Image>(),
                Author = With.Default<Author>(),
            };

            return recipe;
        }

        public static IEnumerable<Recipe> RecipesWithDefaultDataAndCategoryId(int count, int categoryId)
        {
            var recipes = Enumerable
                .Range(0, count)
                .Select(r => RecipeWithDefaultDataAndCategoryId(categoryId));

            return recipes;
        }

        public static IngredientFormModel IngredientFormWithDefaultData()
        {
            var ingredient = new IngredientFormModel
            {
                Index = 1,
                Name = "ing",
                Quantity = 1,
                MeasurementUnitId = 1,
            };

            return ingredient;
        }

        public static IEnumerable<IngredientFormModel> IngredientFormsWithDefaultData(int count)
        {
            var ingredients = Enumerable
                .Range(0, count)
                .Select(r => IngredientFormWithDefaultData());

            return ingredients;
        }

        public static RecipeEditFormModel RecipeEditFormWithData()
        {
            var recipe = new RecipeEditFormModel
            {
                Title = "Title",
                Content = new string('*', Recipes.ContentMinLength),
                CategoryId = 1,
                Ingredients = IngredientFormsWithDefaultData(1),
            };

            return recipe;
        }

        public static IEnumerable<Category> CategoriesWithDefaultData(int count)
        {
            var index = 1;
            var categories = Enumerable
                .Range(0, count)
                .Select(r => new Category
                {
                    Name = $"cat-{index}"
                });

            return categories;
        }
    }
}
