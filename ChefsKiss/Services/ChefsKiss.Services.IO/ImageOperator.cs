namespace ChefsKiss.Services.IO
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    using static ChefsKiss.Common.WebConstants;

    public class ImageOperator : IImageOperator
    {
        private readonly IWebHostEnvironment environment;

        private readonly string imagesPath;

        public ImageOperator(IWebHostEnvironment environment)
        {
            this.environment = environment;
            // FIXME: This is so unit tests pass. But what does it even have to do with the HomeController???
            var webroot = environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            this.imagesPath = Path.Combine(webroot, ImagesDirectory);

            Directory.CreateDirectory(this.imagesPath);
        }

        public async Task WriteAsync(IFormFile file, string fileName, string extension)
        {
            var fileNameAndExtension = $"{fileName}.{extension}";

            var path = Path.Combine(this.imagesPath, fileNameAndExtension);
            using Stream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        public async Task WriteAsync(byte[] file, string name, string extension)
        {
            var path = Path.Combine(this.imagesPath, $"{name}.{extension}");
            await File.WriteAllBytesAsync(path, file);
        }

        public void Delete(string name, string extension)
        {
            var fileName = $"{name}.{extension}";
            var path = Path.Combine(this.imagesPath, fileName);

            File.Delete(path);
        }
    }
}
