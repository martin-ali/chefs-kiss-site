namespace ChefsKiss.Web.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Common;
    using ChefsKiss.Web.Models.Categories;

    public class RecipesSearchModel
    {
        [Required]
        public string SearchTerm { get; init; }

        public int CategoryId { get; init; }

        public SortBy SortBy { get; init; }

        public IEnumerable<CategorySelectViewModel> Categories { get; set; }

        public IEnumerable<RecipeListViewModel> Recipes { get; init; } = new List<RecipeListViewModel>();
    }
}
