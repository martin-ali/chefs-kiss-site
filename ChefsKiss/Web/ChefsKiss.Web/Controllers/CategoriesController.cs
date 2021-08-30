namespace ChefsKiss.Web.Controllers
{
    using System;
    using System.Collections.Generic;

    using ChefsKiss.Web.Models.Categories;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using static ChefsKiss.Common.WebConstants;
    using static ChefsKiss.Common.WebConstants.Cache;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categories;
        private readonly IRecipesService recipes;
        private readonly IMemoryCache cache;

        public CategoriesController(ICategoriesService categories, IRecipesService recipes, IMemoryCache cache)
        {
            this.categories = categories;
            this.recipes = recipes;
            this.cache = cache;
        }

        public IActionResult Details(int id)
        {
            var categories = this.categories.ById<CategoryDetailsViewModel>(id);

            if (categories == null)
            {
                return this.NotFound();
            }

            var recipes = this.recipes.PagedByCategoryId<RecipeListViewModel>(0, ItemsPerPage, id);
            categories.Recipes = recipes;

            return this.View(categories);
        }

        public IActionResult Explore()
        {
            var categories = this.cache.Get<IEnumerable<CategoryCarouselViewModel>>(CategoriesExploreCacheKey);

            if (categories == null)
            {
                categories = this.categories.All<CategoryCarouselViewModel>();
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                this.cache.Set(CategoriesExploreCacheKey, categories, cacheOptions);
            }

            return this.View(categories);
        }
    }
}
