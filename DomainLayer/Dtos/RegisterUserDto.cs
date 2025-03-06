using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Dtos
{
    public class RegisterUserDto : LoginUserDto
    {
        [Required(ErrorMessage = "UserName is Required")]
        [StringLength(40,MinimumLength = 5,ErrorMessage = "UserName is not accepted")]
        public string UserName { get; set; }
    }
}
