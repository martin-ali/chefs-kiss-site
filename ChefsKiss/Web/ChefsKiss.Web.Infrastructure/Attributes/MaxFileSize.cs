namespace ChefsKiss.Web.Infrastructure.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using ChefsKiss.Common;

    using Microsoft.AspNetCore.Http;

    public class MaxFileSize : ValidationAttribute
    {
        private readonly int maxAllowedBytes;

        public MaxFileSize(int bytes)
            : base()
        {
            this.maxAllowedBytes = bytes;

            var size = Helpers.GetReadableFileSize(bytes);
            this.ErrorMessage = $"File size has exceeded the allowed maximum of {size}";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = (IFormFile)value;

            if (file.Length > this.maxAllowedBytes)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
