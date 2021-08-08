namespace ChefsKiss.Web.Areas.Identity.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserLoginFormModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
