
using System.ComponentModel.DataAnnotations;
using DomainLayer;

namespace ApplicationLayer.Dtos.Order_DTOs
{
    public class AddOrderDto
    {
        [Required]
        [StringLength(200, MinimumLength = 25, ErrorMessage = "ProductName is not accepted")]
        public string ProductName { get; set; } = null!;

        [Required]
        [Range(1,1000)]
        public int Amount { get; set; }
    }
}
