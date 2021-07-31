namespace ChefsKiss.Web.Areas.Recipes.Models.Reviews
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class ReviewServiceModel : IMapFrom<Review>
    {
        public string AuthorId { get; init; }
    }
}
