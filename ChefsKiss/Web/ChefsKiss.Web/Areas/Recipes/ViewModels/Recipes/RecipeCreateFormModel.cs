namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class RecipeCreateFormModel
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [MaxLength(10000)]
        [MinLength(100)]
        public string Content { get; init; }

        [Required]
        public IFormFile ImageUrl { get; init; }
    }
}
