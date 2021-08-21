namespace ChefsKiss.Web.Models.Ingredients
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    using static ChefsKiss.Common.DataConstants;
    using static ChefsKiss.Common.ErrorMessages;

    public class IngredientFormModel : IMapFrom<RecipeIngredient>, IHaveCustomMappings
    {
        [Required]
        public int Index { get; set; }

        [Required]
        [StringLength(Ingredients.NameMaxLength, MinimumLength = Ingredients.NameMinLength, ErrorMessage = LengthBetween)]
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
