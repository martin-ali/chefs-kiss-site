namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task<Image> CreateImageAsync(IFormFile input);

        void Delete(int imageId);

        string ImagePath(int imageId);
    }
}
