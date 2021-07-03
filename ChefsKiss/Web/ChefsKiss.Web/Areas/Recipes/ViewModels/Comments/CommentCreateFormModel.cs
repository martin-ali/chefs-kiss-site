namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentCreateFormModel
    {
        public int RecipeId { get; init; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Content { get; init; }
    }
}
