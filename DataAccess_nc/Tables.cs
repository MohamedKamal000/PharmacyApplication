namespace DomainLayer
{
   
    public class Users
    {
        public int Id { get; private set; } = -1;
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } 
        public bool Role { get; set; }

        public override string ToString()
        {
            return $@"id: {Id}
                      PhoneNumber: {PhoneNumber}
                      UserName: {UserName}
                      Password: {Password}
                      Role: {Role}";
        }
    }

    public class Orders
    {
        public int Id { get; private set; } = -1;
        
        public Users User { get; set; }

        public List<MedicalProducts> Products { get; set; } = new List<MedicalProducts>();
        
        public int DeliveryManID { get; set; }
        
        public int Amount { get; set; }
        
        public decimal DeliveryPrice { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public string Status { get; set; }

        public override string ToString()
        {
            // Convert the list of products to a string
            string productsString = string.Join(", ", Products.Select(p => p.ToString()));

            // Build the final string representation
            return $"Order ID: {Id}\n" +
                   $"User: {User?.ToString() ?? "No User"}\n" +
                   $"Products: [{productsString}]\n" +
                   $"DeliveryManID: {DeliveryManID}\n" +
                   $"Amount: {Amount}\n" +
                   $"DeliveryPrice: {DeliveryPrice}\n" +
                   $"TotalPrice: {TotalPrice}\n" +
                   $"OrderDate: {OrderDate}\n" +
                   $"Status: {Status}";
        }
    }

    public class MedicalProducts
    {
        public int Id { get; private set; } = -1;
        
        public string ProductName { get; set; }
        
        public decimal Price { get; set; }
        
        public int Stock { get; set; }

        public string ItemDescription { get; set; }
        
        public int ProductCategory { get; set; }
        
        public int ProductSubCategory { get; set; }

        public override string ToString()
        {
            return $"Product ID: {Id}, Name: {ProductName}, Price: {Price}, Stock: {Stock}, Description: {ItemDescription}, Category: {ProductCategory}, SubCategory: {ProductSubCategory}";
        }
    }

    public class Delivery
    {
        public int Id { get; private set; } = -1;

        public string PhoneNumber { get; set; }
        
        public string DeliveryManName { get; set; }
    }

    public class MedicalCategory
    {
        public int Id { get; private set; } = -1;

        public string CategoryName { get; set; }
    }

    public class SubMedicalCategory
    {
        public int Id { get; private set; } = -1;
        
        public int MainCategory { get; set; }

        public string SubMedicalCategoryName { get; set; }
    }
}