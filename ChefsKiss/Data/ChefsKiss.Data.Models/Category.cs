namespace ChefsKiss.Data.Models
{
    using System.Collections.Generic;

    using ChefsKiss.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public string Name { get; init; }

        public IEnumerable<Recipe> Recipes { get; init; }
    }
}
