namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class ReviewCreateFormModel
    {
        [Required]
        public int RecipeId { get; init; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Content { get; init; }

        [Required]
        public int Rating { get; init; }
    }
}
