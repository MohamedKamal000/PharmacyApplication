using DomainLayer;

namespace ApplicationLayer.Dtos.User_DTOs
{
    public class UserOrderDto
    {
        public GetUserDto User { get; set; } // one time

        public List<OrderedProducts> Products { get; set; } = new List<OrderedProducts>();// more than once

        public int DeliveryManID { get; set; } // one time

        public decimal DeliveryPrice { get; set; } // one time

        public decimal TotalPrice { get; set; } // one time

        public DateTime OrderDate { get; set; } // one time

        public string Status { get; set; } // one time

    }
}
