namespace ChefsKiss.Web.Areas.Recipes.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ChefsKiss.Data.Common.Repositories;
    using ChefsKiss.Data.Models;
    using ChefsKiss.Services.IO;
    using ChefsKiss.Services.Validation;

    using Microsoft.AspNetCore.Http;

    using static ChefsKiss.Common.GlobalConstants;

    public class ImagesService : IImagesService
    {
        private readonly IRepository<Image> imagesRepository;
        private readonly IImageOperator imageOperator;
        private readonly IFileValidator fileValidator;

        public ImagesService(
            IRepository<Image> imagesRepository,
            IImageOperator imageOperator,
            IFileValidator fileValidator)
        {
            this.imagesRepository = imagesRepository;
            this.imageOperator = imageOperator;
            this.fileValidator = fileValidator;
        }

        public async Task<Image> CreateImage(IFormFile input, string authorId)
        {
            var extension = Path.GetExtension(input.FileName).TrimStart('.');

            // FIXME: Validation in service, move it out
            this.fileValidator.ThrowIfExtensionIsInvalid(extension);

            var image = new Image
            {
                Name = Guid.NewGuid().ToString(),
                Extension = extension,
            };

            await this.imageOperator.Write(input, image.Name, extension);
            await this.imagesRepository.AddAsync(image);

            await this.imagesRepository.SaveChangesAsync();

            return image;
        }

        public async Task DeleteImage(int imageId)
        {
            var image = this.imagesRepository.All()
                .FirstOrDefault(i => i.Id == imageId);

            this.imagesRepository.Delete(image);
            await this.imagesRepository.SaveChangesAsync();
        }

        public string GetRelativeImagePath(int imageId)
        {
            string fileName = this.GetFileName(imageId);
            var path = Path.Combine(ImagesDirectory, fileName);

            return path;
        }

        private string GetFileName(int imageId)
        {
            var extension = this.imagesRepository.All()
                .Where(i => i.Id == imageId)
                .Select(i => $"{i.Id}.{i.Extension}")
                .FirstOrDefault();

            return extension;
        }
    }
}
