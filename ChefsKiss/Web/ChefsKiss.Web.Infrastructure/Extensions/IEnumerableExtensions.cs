namespace ChefsKiss.Web.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Returns average if collection has elements or default otherwise
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>The average of all items in the collection</returns>
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
