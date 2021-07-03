namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Comments;

    public class RecipeDetailsViewModel : RecipeInListViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string Content { get; init; }

        public IEnumerable<CommentListViewModel> Comments { get; init; } = new List<CommentListViewModel>();
    }
}
