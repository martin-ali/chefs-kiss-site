namespace ChefsKiss.Web.Areas.Recipes.Models.Reviews
{
    using System;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class ReviewListViewModel : IMapFrom<Review>
    {
        public string AuthorId { get; init; }

        public string AuthorUsername { get; init; }

        public string Content { get; init; }

        public DateTime CreatedOn { get; init; }

        public int Rating { get; init; }
    }
}
