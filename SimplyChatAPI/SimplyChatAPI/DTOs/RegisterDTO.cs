using System.ComponentModel.DataAnnotations;

namespace SimplyChatAPI.DTOs
{
    public class RegisterDTO
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
