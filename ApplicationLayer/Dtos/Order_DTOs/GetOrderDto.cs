
using ApplicationLayer.Dtos.User_DTOs;

namespace ApplicationLayer.Dtos.Order_DTOs
{
    public class GetOrderDto
    {
        public string Status { get; set; } = null!;

        public GetUserDto User { get; set; } = null!;
    }
}
