namespace ChefsKiss.Services.IO
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileOperator
    {
        Task WriteAsync(IFormFile file, string fileName, string extension);

        Task WriteAsync(byte[] file, string name, string extension);

        void Remove(string name, string extension);
    }
}
