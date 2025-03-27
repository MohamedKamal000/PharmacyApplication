

namespace ApplicationLayer.Dtos.Product_DTOS
{
    public class CreateProductDto
    {
        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string ItemDescription { get; set; } = null!;

        public int ProductCategory { get; set; }

        public int ProductSubCategory { get; set; }

    }
}
