using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Dtos.User_DTOs
{
    public class RegisterUserDto : LoginUserDto
    {
        [Required(ErrorMessage = "UserName is Required")]
        [StringLength(40,MinimumLength = 5,ErrorMessage = "UserName is not accepted")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PhoneNumber is Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "PhoneNumber is not accepted")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(200, MinimumLength = 8, ErrorMessage = "Password is not accepted")]
        public string Password { get; set; }
    }
}
