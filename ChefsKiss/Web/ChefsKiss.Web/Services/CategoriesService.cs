namespace ChefsKiss.Web.Services
{
    using System.Collections.Generic;
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
                .FirstOrDefault();

            return category;
        }

        public IEnumerable<T> All<T>()
        {
            var categories = this.data.Categories
                .OrderBy(x => x.Name)
                .MapTo<T>()
                .ToList();

            return categories;
        }
    }
}
