namespace ChefsKiss.Web.Models.Recipes
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class RecipeCreateFormModel : RecipeFormModel
    {
        [Required]
        public override IFormFile Image { get; init; }
    }
}
