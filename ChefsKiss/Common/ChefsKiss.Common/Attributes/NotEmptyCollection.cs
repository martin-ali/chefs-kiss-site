namespace ChefsKiss.Common.Attributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class NotEmptyCollection : ValidationAttribute
    {
        public NotEmptyCollection()
            : base()
        {
            this.ErrorMessage = "Collection cannot be empty.";
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
