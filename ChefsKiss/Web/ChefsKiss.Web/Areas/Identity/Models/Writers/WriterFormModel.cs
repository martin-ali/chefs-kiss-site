namespace ChefsKiss.Web.Areas.Identity.Models.Writers
{
    using System.ComponentModel.DataAnnotations;

    using static ChefsKiss.Common.DataConstants;

    public class WriterFormModel
    {
        [Required]
        [MinLength(Writers.NameMinLength)]
        [MaxLength(Writers.NameMaxLength)]
        [Display(Name = "First name")]
        public string FirstName { get; init; }

        [Required]
        [MinLength(Writers.NameMinLength)]
        [MaxLength(Writers.NameMaxLength)]
        [Display(Name = "Last name")]
        public string LastName { get; init; }
    }
}
