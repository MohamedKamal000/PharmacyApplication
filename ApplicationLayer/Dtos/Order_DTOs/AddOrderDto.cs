
using System.ComponentModel.DataAnnotations;
using DomainLayer;

namespace ApplicationLayer.Dtos.Order_DTOs
{
    public class AddOrderDto
    {
        [Required] public string ProductName { get; set; } = null!;

        [Required]
        public int Amount { get; set; }
    }
}
