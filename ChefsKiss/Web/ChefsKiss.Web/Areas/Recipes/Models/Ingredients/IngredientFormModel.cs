namespace ChefsKiss.Web.Areas.Recipes.Models.Ingredients
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;
    using static ChefsKiss.Common.DataConstants;

    public class IngredientFormModel : IMapFrom<RecipeIngredient>, IHaveCustomMappings
    {
        [Required]
        public int Index { get; set; }

        [Required]
        [MinLength(Ingredients.NameMinLength)]
        [MaxLength(Ingredients.NameMaxLength)]
        public string Name { get; init; }

        [Required]
        [Range(RecipeIngredients.MinQuantity, RecipeIngredients.MaxQuantity)]
        public double Quantity { get; init; }

        [Required]
        public int MeasurementUnitId { get; init; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RecipeIngredient, IngredientFormModel>()
            .ForMember(vm => vm.Name, cfg => cfg.MapFrom(m => m.Ingredient.Name));
        }
    }
}
