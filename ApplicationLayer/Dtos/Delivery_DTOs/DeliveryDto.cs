

using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Dtos.Delivery_DTOs
{
    public class DeliveryDto
    {
        [Required(ErrorMessage = "PhoneNumber is Required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number is not accepted")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name is Not Accepted")]
        public string DeliveryManName { get; set; } = null!;
    }
}
