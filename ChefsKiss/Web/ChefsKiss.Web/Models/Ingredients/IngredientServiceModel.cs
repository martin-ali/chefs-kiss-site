namespace ChefsKiss.Web.Models.Ingredients
{
    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class IngredientServiceModel : IMapFrom<Ingredient>, IMapFrom<IngredientFormModel>
    {
        public string Name { get; init; }

        public double Quantity { get; init; }

        public int MeasurementUnitId { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RecipeIngredient, IngredientFormModel>()
            .ForMember(vm => vm.Name, cfg => cfg.MapFrom(m => m.Ingredient.Name));
        }
    }
}
