namespace ChefsKiss.Tests.Data
{
    using System.Collections.Generic;

    using System.Linq;

    using ChefsKiss.Data.Common.Models;
    using ChefsKiss.Data.Models;

    public class Items
    {
        public static IEnumerable<T> TenItems<T>() where T : BaseModel<int>, new()
        {
            var recipes = Enumerable
                .Range(0, 10)
                .Select(x => new T { Id = x });

            return recipes;
        }
    }
}
