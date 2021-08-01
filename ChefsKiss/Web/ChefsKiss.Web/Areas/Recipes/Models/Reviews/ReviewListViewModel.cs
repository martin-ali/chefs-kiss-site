namespace ChefsKiss.Web.Areas.Recipes.Models.Reviews
{
    using System;
    using AutoMapper;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class ReviewListViewModel : IMapFrom<Review>, IHaveCustomMappings
    {
        public string AuthorId { get; init; }

        public string AuthorUsername { get; init; }

        public string AuthorFullname { get; init; }

        public string Content { get; init; }

        public DateTime CreatedOn { get; init; }

        public int Rating { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<Review, ReviewListViewModel>()
            .ForMember(vm => vm.AuthorFullname, cfg => cfg.MapFrom(m => $"{m.Author.FirstName} {m.Author.LastName}"));
        }
    }
}
