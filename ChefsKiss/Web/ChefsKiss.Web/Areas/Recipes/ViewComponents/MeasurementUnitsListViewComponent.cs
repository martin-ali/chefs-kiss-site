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

        public IViewComponentResult Invoke(int selected = 0)
        {
            var items = this.measurementUnitsService.All<MeasurementUnitViewModel>();
            var model = new MeasurementUnitsComponentModel
            {
                Selected = selected,
                Units = items,
            };

            return this.View(model);
        }
    }
}
