using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Models.DTOs
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Full name")]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "{0} is incorrect")]
        [Required(ErrorMessage = "{0} must be required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="{0} must be required")]
        [StringLength(18, MinimumLength =8, ErrorMessage = "{0} is at least {2} and max is {1} characters")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Re-enter password is incorrect")]
        [Display(Name = "Re-enter password")]
        public string ReenterPassword { get; set; } = string.Empty;
    }
}
