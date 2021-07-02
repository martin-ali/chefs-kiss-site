namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Threading.Tasks;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Web.Areas.Recipes.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> commentsRepository;

        public CommentsService(IRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task CreateAsync(CommentCreateFormModel input, string authorId)
        {
            var comment = new Comment
            {
                AuthorId = authorId,
                RecipeId = input.RecipeId,
                Content = input.Content,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
