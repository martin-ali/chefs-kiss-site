namespace ChefsKiss.Web.Services
{
    using System.Linq;

    using ChefsKiss.Data;
    using ChefsKiss.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly RecipesDbContext data;

        public CategoriesService(RecipesDbContext data)
        {
            this.data = data;

        }

        public T ById<T>(int id)
        {
            var category = this.data.Categories
                .Where(x => x.Id == id)
                .MapTo<T>()
                .First();

            return category;
        }
    }
}
