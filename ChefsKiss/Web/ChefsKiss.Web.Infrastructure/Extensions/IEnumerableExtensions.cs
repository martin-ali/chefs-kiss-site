namespace ChefsKiss.Web.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtensions
    {
        public static double AverageOrDefault(this IEnumerable<int> collection)
        {
            if (collection.Any() == false)
            {
                return default;
            }

            return collection.Average();
        }
    }
}
