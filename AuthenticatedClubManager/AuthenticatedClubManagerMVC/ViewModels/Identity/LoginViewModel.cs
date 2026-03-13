using System.ComponentModel.DataAnnotations;

namespace AuthenticatedClubManagerMVC.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
