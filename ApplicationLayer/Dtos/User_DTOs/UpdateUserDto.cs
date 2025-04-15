using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Dtos.User_DTOs;

public class UpdateUserDto
{
    [Required(ErrorMessage = "PhoneNumber is Required")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "PhoneNumber is not accepted")]
    public string PhoneNumber { get; set; }
    
    
    [Required(ErrorMessage = "UserName is Required")]
    [StringLength(40,MinimumLength = 5,ErrorMessage = "UserName is not accepted")]
    public string NewUserName { get; set; }
}