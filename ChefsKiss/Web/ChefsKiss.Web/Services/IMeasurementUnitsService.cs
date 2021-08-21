namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;

    public interface IMeasurementUnitsService
    {
        IEnumerable<T> All<T>();
    }
}
