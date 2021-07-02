namespace ChefsKiss.Web.Areas.Recipes.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeCreateFormModel
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [MaxLength(10000)]
        // [MinLength(1000)]
        public string Content { get; init; }
    }
}
