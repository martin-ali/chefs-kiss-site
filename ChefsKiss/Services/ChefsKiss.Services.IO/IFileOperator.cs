namespace ChefsKiss.Services.IO
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileOperator
    {
        Task Write(IFormFile file, string fileName, string extension);

        Task Write(byte[] file, string name, string extension);

        void Delete(string id, string extension);
    }
}
