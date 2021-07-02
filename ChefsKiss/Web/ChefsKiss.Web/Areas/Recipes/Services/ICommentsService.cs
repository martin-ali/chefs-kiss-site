namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Recipes.ViewModels.Comments;

    public interface ICommentsService
    {
        Task CreateAsync(CommentCreateFormModel input, string authorId);
    }
}
