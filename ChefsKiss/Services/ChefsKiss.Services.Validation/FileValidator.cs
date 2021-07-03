namespace ChefsKiss.Services.Validation
{
    using System.IO;
    using System.Linq;

    using ChefsKiss.Common;

    public class FileValidator : IFileValidator
    {
        public void ThrowIfExtensionIsInvalid(string extension)
        {
            if (GlobalConstants.AllowedImageExtensions.Any(x => extension.EndsWith(x)) == false)
            {
                throw new InvalidDataException($"Disallowed extension {extension}. Allowed extensions are {string.Join(", ", GlobalConstants.AllowedImageExtensions)}");
            }
        }
    }
}
