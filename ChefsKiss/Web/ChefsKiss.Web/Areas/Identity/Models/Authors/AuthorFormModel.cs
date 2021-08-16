namespace ChefsKiss.Web.Areas.Identity.Models.Authors
{
    using System.ComponentModel.DataAnnotations;

    using static ChefsKiss.Common.DataConstants;
    using static ChefsKiss.Common.ErrorMessages;

    public class AuthorFormModel
    {
        [Required]
        [Display(Name = "First name")]
        [StringLength(Authors.NameMaxLength, MinimumLength = Authors.NameMinLength, ErrorMessage = LengthBetween)]
        public string FirstName { get; init; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(Authors.NameMaxLength, MinimumLength = Authors.NameMinLength, ErrorMessage = LengthBetween)]
        public string LastName { get; init; }
    }
}
