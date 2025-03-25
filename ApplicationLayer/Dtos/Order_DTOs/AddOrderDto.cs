
using System.ComponentModel.DataAnnotations;
using DomainLayer;

namespace ApplicationLayer.Dtos.Order_DTOs
{
    public class AddOrderDto
    {
        [Required]
        public Product Product { get; set; } = new Product();

        [Required]
        public int Amount { get; set; }
    }
}
