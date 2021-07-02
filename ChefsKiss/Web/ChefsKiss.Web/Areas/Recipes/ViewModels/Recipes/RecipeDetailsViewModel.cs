namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Comments;

    public class RecipeDetailsViewModel : IMapFrom<Recipe>
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Content { get; init; }

        public IEnumerable<CommentListViewModel> Comments { get; init; }
    }
}
