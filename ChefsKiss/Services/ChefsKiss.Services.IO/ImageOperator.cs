namespace ChefsKiss.Services.IO
{
    using System.IO;
    using System.Threading.Tasks;

    using ChefsKiss.Common;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class ImageOperator : IImageOperator
    {
        private readonly IWebHostEnvironment environment;

        private readonly string imagesPath;

        public ImageOperator(IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.imagesPath = Path.Combine(this.environment.WebRootPath, GlobalConstants.ImagesDirectory);

            Directory.CreateDirectory(this.imagesPath);
        }

        public async Task Write(IFormFile file, string fileName, string extension)
        {
            var fileNameAndExtension = $"{fileName}.{extension}";

            var path = Path.Combine(this.imagesPath, fileNameAndExtension);
            using Stream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        public async Task Write(byte[] file, string name, string extension)
        {
            var path = Path.Combine(this.imagesPath, $"{name}.{extension}");
            await File.WriteAllBytesAsync(path, file);
        }

        public void Delete(string id, string extension)
        {
            var fileName = $"{id}.{extension}";
            var path = Path.Combine(this.imagesPath, fileName);

            File.Delete(path);
        }
    }
}
