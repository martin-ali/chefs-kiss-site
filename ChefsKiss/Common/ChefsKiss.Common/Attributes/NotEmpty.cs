namespace ChefsKiss.Common.Attributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class NotEmpty : ValidationAttribute
    {
        public NotEmpty()
            : base()
        {
            this.ErrorMessage = ErrorMessages.EmptyCollection;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var collection = (IEnumerable<object>)value;

            if (collection.Any() == false)
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
