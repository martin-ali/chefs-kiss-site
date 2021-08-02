namespace ChefsKiss.Web.Areas.Recipes.ViewComponents
{
    using System.Threading.Tasks;

    using ChefsKiss.Web.Areas.Recipes.Models.MeasurementUnits;
    using ChefsKiss.Web.Areas.Recipes.Services;

    using Microsoft.AspNetCore.Mvc;

    public class MeasurementUnitsListViewComponent : ViewComponent
    {
        private readonly IMeasurementUnitsService measurementUnitsService;

        public MeasurementUnitsListViewComponent(IMeasurementUnitsService measurementUnitsService)
        {
            this.measurementUnitsService = measurementUnitsService;
        }

        // NOTE: The method being async was a requirement, not my own decision
        public async Task<IViewComponentResult> InvokeAsync(int selected = 0)
        {
            var items = this.measurementUnitsService.GetAll<MeasurementUnitViewModel>();
            var model = new MeasurementUnitsComponentModel
            {
                Selected = selected,
                Units = items,
            };

            return this.View(model);
        }
    }
}
