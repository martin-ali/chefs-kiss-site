namespace ChefsKiss.Web.Areas.Recipes.Models.Reviews
{
    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class ReviewDetailsViewModel : ReviewBaseViewModel, IMapFrom<Review>, IHaveCustomMappings
    {
        public string Content { get; init; }
    }
}
