namespace ChefsKiss.Web.Models.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Common;
    using ChefsKiss.Web.Models.Categories;

    public class RecipesQueryModel
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Search term")]
        public string SearchTerm { get; init; }

        public int CategoryId { get; init; }

        public RecipesSortBy SortBy { get; init; }

        public bool HasBeenQueried { get; set; }

        public IEnumerable<CategorySelectViewModel> Categories { get; set; } = new List<CategorySelectViewModel>();

        public IEnumerable<RecipeListViewModel> Recipes { get; set; } = new List<RecipeListViewModel>();
    }
}
