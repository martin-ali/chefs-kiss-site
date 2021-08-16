namespace ChefsKiss.Web.Areas.Recipes.Models.Reviews
{
    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    using static ChefsKiss.Common.WebConstants;

    public class ReviewListViewModel : ReviewBaseViewModel, IMapFrom<Review>, IHaveCustomMappings
    {
        public string Summary { get; init; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Review, ReviewListViewModel>()
            .ForMember(vm => vm.Summary, cfg => cfg.MapFrom(m => m.Content.Substring(0, ReviewSummaryLength)));
        }
    }
}
