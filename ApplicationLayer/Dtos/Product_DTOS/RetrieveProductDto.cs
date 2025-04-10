namespace ApplicationLayer.Dtos.Product_DTOS;

public class RetrieveProductDto
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int Amount { get; set; }
    
    public decimal Price { get; set; }
        
    public int Stock { get; set; }
    
    public int ProductCategory { get; set; }
        
    public int ProductSubCategory { get; set; }
}