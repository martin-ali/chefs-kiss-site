namespace ChefsKiss.Web.Areas.Recipes.Models.Recipes
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using ChefsKiss.Web.Areas.Recipes.Models.Ingredients;
    using ChefsKiss.Web.Areas.Recipes.Models.Reviews;

    public class RecipeDetailsViewModel : RecipeBaseViewModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public string AuthorUsername { get; set; }

        public string Content { get; init; }

        public bool UserHasPostedReview { get; set; }

        public IEnumerable<ReviewListViewModel> Reviews { get; init; } = new List<ReviewListViewModel>();

        public IEnumerable<IngredientViewModel> Ingredients { get; init; } = new List<IngredientViewModel>();

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeDetailsViewModel>()
                .ForMember(vm => vm.Reviews, cfg => cfg.MapFrom(m => m.Reviews.OrderByDescending(c => c.CreatedOn)))
                .ForMember(vm => vm.Ingredients, cfg => cfg.MapFrom(m => m.RecipeIngredients))
                .ForMember(vm => vm.AuthorUsername, cfg => cfg.MapFrom(m => m.Author.User.UserName));
        }
    }
}
