namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Collections.Generic;

    public interface IMeasurementUnitsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
