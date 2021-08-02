namespace ChefsKiss.Web.Areas.Recipes.Models.Reviews
{
    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    using static ChefsKiss.Common.GlobalConstants;

    public class ReviewListViewModel : ReviewBaseViewModel, IMapFrom<Review>, IHaveCustomMappings
    {
        public int Id { get; init; }

        public string Summary { get; init; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<Review, ReviewListViewModel>()
            .ForMember(vm => vm.Summary, cfg => cfg.MapFrom(m => m.Content.Substring(0, ReviewSummaryLength)));
        }
    }
}
