namespace ChefsKiss.Web.Infrastructure.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Microsoft.AspNetCore.Http;

    public class ImagesOnly : ValidationAttribute
    {
        private readonly string[] AllowedExtensions = new[] { "png", "jpg", "jpeg" };

        public ImagesOnly()
            : base()
        {
            this.ErrorMessage = $"This file type is not allowed. Allowed types are: {string.Join(", ", this.AllowedExtensions)}.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = (IFormFile)value;
            var contentType = file.ContentType.ToLower();
            var contentData = contentType.Split('/');

            if (contentData.Length != 2)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            var extension = contentData[1].Trim();

            if (AllowedExtensions.Contains(extension))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(this.ErrorMessage);
        }
    }
}
