namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Comments
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class CommentListViewModel : IMapFrom<Comment>
    {
        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string Content { get; set; }
    }
}
