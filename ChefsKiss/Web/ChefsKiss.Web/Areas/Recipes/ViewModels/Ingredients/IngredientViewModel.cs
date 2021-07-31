namespace ChefsKiss.Web.Areas.Recipes.ViewModels.Ingredients
{
    using AutoMapper;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class IngredientViewModel : IMapFrom<RecipeIngredient>, IHaveCustomMappings
    {
        public string Name { get; init; }

        public double Quantity { get; init; }

        public string MeasurementUnit { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
            .CreateMap<RecipeIngredient, IngredientViewModel>()
            .ForMember(vm => vm.MeasurementUnit, cfg => cfg.MapFrom(m => m.MeasurementUnit.Name))
            .ForMember(vm => vm.Name, cfg => cfg.MapFrom(m => m.Ingredient.Name));
        }
    }
}
