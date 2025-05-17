using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Dtos.Order_DTOs;

public class AcceptOrderDto
{
    [Required(ErrorMessage = "UserPhoneNumber is Required")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "UserPhoneNumber is not accepted")]
    public string UserPhoneNumber { get; set; }
    
    [Required(ErrorMessage = "DeliveryPhoneNumber is Required")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "DeliveryPhoneNumber is not accepted")]
    public string DeliveryPhoneNumber { get; set; }

}