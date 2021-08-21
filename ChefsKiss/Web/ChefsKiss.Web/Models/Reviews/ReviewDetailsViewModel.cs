namespace ChefsKiss.Web.Models.Reviews
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class ReviewDetailsViewModel : ReviewBaseViewModel, IMapFrom<Review>, IHaveCustomMappings
    {
        public string Content { get; init; }
    }
}
