using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Dtos
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "PhoneNumber is Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Password is Required")]
        [StringLength(200, MinimumLength = 8, ErrorMessage = "Password Length is not valid")]
        public string Password { get; set; }
    }

}
