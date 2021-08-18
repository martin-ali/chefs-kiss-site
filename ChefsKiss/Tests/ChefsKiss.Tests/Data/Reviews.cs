namespace ChefsKiss.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ChefsKiss.Data.Models;

    public class Reviews
    {
        public IEnumerable<Review> GetReviews()
        {
            var reviews = Enumerable
               .Range(0, 10)
               .Select(x => new Review());

            return reviews;
        }
    }
}
