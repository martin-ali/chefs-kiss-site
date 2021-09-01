namespace ChefsKiss.Web.Services
{
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task<Image> CreateImageAsync(IFormFile input);

        void Remove(int imageId);

        void RemoveFileOnly(int imageId);
    }
}
