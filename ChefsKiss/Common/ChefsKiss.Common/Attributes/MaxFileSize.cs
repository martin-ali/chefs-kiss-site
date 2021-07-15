namespace ChefsKiss.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class MaxFileSize : ValidationAttribute
    {
        private readonly int maxAllowedBytes;

        public MaxFileSize(int bytes)
        {
            this.maxAllowedBytes = bytes;
            this.ErrorMessage = "File size has exceeded the allowed maximum.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = (IFormFile)value;

            if (file.Length > this.maxAllowedBytes)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return new ValidationResult(this.ErrorMessage);
        }
    }
}
