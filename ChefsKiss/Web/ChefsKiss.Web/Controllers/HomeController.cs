namespace ChefsKiss.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using ChefsKiss.Web.Models;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Services;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using static ChefsKiss.Common.WebConstants;
    using static ChefsKiss.Common.WebConstants.Cache;

    public class HomeController : Controller
    {
        private readonly IRecipesService recipes;
        private readonly IMemoryCache cache;

        public HomeController(IRecipesService recipes, IMemoryCache cache)
        {
            this.cache = cache;
            this.recipes = recipes;
        }

        [HttpGet("/")]
        [HttpGet("/Index")]
        public IActionResult Index()
        {
            var recipes = this.cache.Get<IEnumerable<RecipeListViewModel>>(PopularRecipesCacheKey);

            if (recipes == null)
            {
                recipes = this.recipes.Popular<RecipeListViewModel>(PopularRecipesCount);
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                this.cache.Set(PopularRecipesCacheKey, recipes, cacheOptions);
            }

            return this.View(recipes);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
