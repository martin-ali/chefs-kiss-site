namespace ChefsKiss.Tests.Data
{
    using System.Collections.Generic;

    using System.Linq;

    using ChefsKiss.Data.Common.Models;
    using ChefsKiss.Data.Models;

    public class Items
    {
        public static IEnumerable<T> ModelMocks<T>(int count) where T : new()
        {
            var recipes = Enumerable
                .Range(0, count)
                .Select(x => new T());

            return recipes;
        }

        public static IEnumerable<Author> AuthorsWithUsers(int count)
        {
            var recipes = Enumerable
                .Range(0, count)
                .Select(x => new Author { User = new ApplicationUser() });

            return recipes;
        }
    }
}
