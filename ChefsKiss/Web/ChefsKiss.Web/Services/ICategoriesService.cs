namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        T ById<T>(int id);

        IEnumerable<T> All<T>();
    }
}
