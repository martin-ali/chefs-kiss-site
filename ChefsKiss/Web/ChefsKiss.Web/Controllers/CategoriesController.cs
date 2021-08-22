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
            var model = this.categories.ById<CategoryDetailsViewModel>(id);
            var recipes = this.recipes.PagedByCategoryId<RecipeListViewModel>(0, ItemsPerPage, id);
            model.Recipes = recipes;

            return this.View(model);
        }

        // Caching
        public IActionResult Explore()
        {
            var model = this.cache.Get<IEnumerable<CategoryCarouselViewModel>>(CategoriesExploreCacheKey);

            if (model == null)
            {
                model = this.categories.All<CategoryCarouselViewModel>();
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                this.cache.Set(CategoriesExploreCacheKey, model, cacheOptions);
            }

            return this.View(model);
        }
    }
}
