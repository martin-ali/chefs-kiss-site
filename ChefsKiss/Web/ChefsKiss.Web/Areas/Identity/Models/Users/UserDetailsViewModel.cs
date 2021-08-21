namespace ChefsKiss.Web.Areas.Identity.Models.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Models.Recipes;
    using ChefsKiss.Web.Models.Reviews;

    public class UserDetailsViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; init; }

        public string UserName { get; init; }

        public int RecipesCount { get; set; }

        public int ReviewsCount { get; init; }

        public IEnumerable<RecipeListViewModel> Recipes { get; set; }

        public IEnumerable<ReviewListViewModel> Reviews { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserDetailsViewModel>()
            .ForMember(vm => vm.ReviewsCount, cfg => cfg.MapFrom(m => m.Reviews.Count()));
        }
    }
}
