namespace ChefsKiss.Services.Validation
{
    using System.IO;
    using System.Linq;

    using static ChefsKiss.Common.DataConstants;

    public class FileValidator : IFileValidator
    {
        private const char ExtensionSeparator = ',';

        public void ThrowIfExtensionIsInvalid(string extension)
        {
            if (AllowedImageExtensions.Split(ExtensionSeparator).Any(x => extension.EndsWith(x)) == false)
            {
                throw new InvalidDataException($"Disallowed extension {extension}. Allowed extensions are {AllowedImageExtensions.Replace(",", ", ")}");
            }
        }
    }
}
