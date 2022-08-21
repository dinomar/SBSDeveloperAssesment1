using System.ComponentModel.DataAnnotations;

namespace SBSDeveloperAssesment1.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
