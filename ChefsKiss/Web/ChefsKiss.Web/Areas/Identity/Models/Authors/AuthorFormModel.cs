namespace ChefsKiss.Web.Areas.Identity.Models.Authors
{
    using System.ComponentModel.DataAnnotations;

    using static ChefsKiss.Common.DataConstants;

    public class AuthorFormModel
    {
        [Required]
        [MinLength(Authors.NameMinLength)]
        [MaxLength(Authors.NameMaxLength)]
        [Display(Name = "First name")]
        public string FirstName { get; init; }

        [Required]
        [MinLength(Authors.NameMinLength)]
        [MaxLength(Authors.NameMaxLength)]
        [Display(Name = "Last name")]
        public string LastName { get; init; }
    }
}
