namespace ChefsKiss.Web.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.IO;

    using Microsoft.AspNetCore.Http;

    using static ChefsKiss.Common.WebConstants;

    public class ImagesService : IImagesService
    {
        private readonly RecipesDbContext data;
        private readonly IImageOperator imageOperator;

        public ImagesService(RecipesDbContext data, IImageOperator imageOperator)
        {
            this.data = data;
            this.imageOperator = imageOperator;
        }

        private string FileName(int imageId)
        {
            var extension = this.data.Images
                .Where(i => i.Id == imageId)
                .Select(i => $"{i.Id}.{i.Extension}")
                .First();

            return extension;
        }

        public async Task<Image> CreateImageAsync(IFormFile input)
        {
            var extension = Path.GetExtension(input.FileName).TrimStart('.');

            var image = new Image
            {
                Name = Guid.NewGuid().ToString(),
                Extension = extension,
            };

            await this.imageOperator.WriteAsync(input, image.Name, extension);
            this.data.Images.Add(image);

            this.data.SaveChanges();

            return image;
        }

        public string ImagePath(int imageId)
        {
            string fileName = this.FileName(imageId);
            var path = Path.Combine(ImagesDirectory, fileName);

            return path;
        }

        public void Delete(int imageId)
        {
            var image = this.data.Images
                .First(i => i.Id == imageId);

            this.data.Images.Remove(image);
            this.imageOperator.Delete(image.Name, image.Extension);

            this.data.SaveChanges();
        }
    }
}