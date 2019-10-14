using System.ComponentModel.DataAnnotations;

namespace DownTime.CommonModel.Models
{
    public class LoginUserDto
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "* The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* The password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
