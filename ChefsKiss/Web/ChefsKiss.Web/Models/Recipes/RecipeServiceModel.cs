namespace ChefsKiss.Web.Models.Recipes
{
    using AutoMapper;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class RecipeServiceModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; init; }

        public string AuthorId { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeServiceModel>()
                .ForMember(vm => vm.AuthorId, cfg => cfg.MapFrom(m => m.Author.UserId));
        }
    }
}
