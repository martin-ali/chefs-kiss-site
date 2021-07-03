namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Comments
{
    using System;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class CommentListViewModel : IMapFrom<Comment>
    {
        public string AuthorId { get; init; }

        public string AuthorUsername { get; init; }

        public string Content { get; init; }

        public DateTime CreatedOn { get; init; }
    }
}
