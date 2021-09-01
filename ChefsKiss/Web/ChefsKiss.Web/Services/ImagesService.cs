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

    public class ImagesService : IImagesService
    {
        private readonly RecipesDbContext data;
        private readonly IImageOperator imageOperator;

        public ImagesService(RecipesDbContext data, IImageOperator imageOperator)
        {
            this.data = data;
            this.imageOperator = imageOperator;
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

        public void Remove(int imageId)
        {
            var image = this.data.Images
                .First(i => i.Id == imageId);

            this.data.Images.Remove(image);
            this.imageOperator.Remove(image.Name, image.Extension);

            this.data.SaveChanges();
        }
    }
}
