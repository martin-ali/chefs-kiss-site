namespace ChefsKiss.Web.Models.Categories
{
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.Mapping;

    public class CategorySelectViewModel : IMapFrom<Category>
    {
        public int Id { get; init; }

        public string Name { get; init; }
    }
}
