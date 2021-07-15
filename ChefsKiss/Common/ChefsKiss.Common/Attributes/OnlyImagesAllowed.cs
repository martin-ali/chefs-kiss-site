namespace ChefsKiss.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class OnlyImagesAllowed : ValidationAttribute
    {
        private readonly string[] AllowedExtensions = new[] { "png", "jpg", "jpeg" };

        public OnlyImagesAllowed()
        {
            this.ErrorMessage = $"This file type is not allowed. Allowed types are: {string.Join(", ", this.AllowedExtensions)}";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = (IFormFile)value;
            var contentType = file.ContentType.ToLower();

            foreach (var allowedExtension in this.AllowedExtensions)
            {
                var contentData = contentType.Split('/');

                if (contentData.Length != 2)
                {
                    break;
                }

                var extension = contentData[1].Trim();

                if (allowedExtension == extension)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(this.ErrorMessage);
        }
    }
}
