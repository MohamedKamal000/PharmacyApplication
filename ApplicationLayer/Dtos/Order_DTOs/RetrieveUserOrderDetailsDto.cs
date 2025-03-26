using DomainLayer;

namespace ApplicationLayer.Dtos.Order_DTOs
{
    public class RetrieveUserOrderDetailsDto
    {
        public List<Product> Products { get; set; } = new List<Product>();// more than once

        public int? DeliveryManID { get; set; } // one time

        public decimal DeliveryPrice { get; set; } // one time

        public decimal TotalPrice { get; set; } // one time

        public DateTime OrderDate { get; set; } // one time

        public string Status { get; set; } // one time

    }
}
