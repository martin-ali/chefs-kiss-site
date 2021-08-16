namespace ChefsKiss.Web.Infrastructure.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using static ChefsKiss.Common.ErrorMessages;

    public class ConstrictLength : ValidationAttribute
    {
        private readonly int minLength;
        private readonly int maxLength;

        public ConstrictLength(int minLength, int maxLength)
            : base()
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var text = (string)value;
            var textIsValid = this.minLength <= text.Length && text.Length <= this.maxLength;

            if (textIsValid == false)
            {
                var errorMessage = LengthBetweenWithParameters(validationContext.DisplayName, minLength, maxLength);
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
