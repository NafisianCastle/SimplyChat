using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SimplyChatAPI.DTOs
{
    public class SignInRequest
    {

        [Display(Name = "Email Address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
