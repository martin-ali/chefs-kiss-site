namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System.Threading.Tasks;

    using ChefsKiss.Data.Models;

    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task<Image> CreateImage(IFormFile input, string authorId);

        Task DeleteImage(int imageId);

        string GetRelativeImagePath(int imageId);
    }
}
