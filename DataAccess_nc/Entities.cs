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

    
    public class UserOder
    {
        
        public Users User { get; set; } // one time

        public List<OrderedProducts> Products { get; set; } = new List<OrderedProducts>();// more than once
        
        public int DeliveryManID { get; set; } // one time
        
        public decimal DeliveryPrice { get; set; } // one time
        
        public decimal TotalPrice { get; set; } // one time
        
        public DateTime OrderDate { get; set; } // one time
        
        public string Status { get; set; } // one time

        public override string ToString()
        {
            // Convert the list of products to a string
            string prts = string.Join("\n\n", Products.Select(p => p.ToString())); ;
            // Build the final string representation
            return
                   $"User: {User?.ToString() ?? "No User"}\n" +
                   $"Products: [{prts}]\n" +
                   $"DeliveryManID: {DeliveryManID}\n" +
                   $"DeliveryPrice: {DeliveryPrice}\n" +
                   $"TotalPrice: {TotalPrice}\n" +
                   $"OrderDate: {OrderDate}\n" +
                   $"Status: {Status}";
        }
    }

    public class Orders // this one used only to Retrieve all orders in db 
    {
        public string UserPhoneNumber { get; set; }

        public string UserName { get; set; }

        public string OrderStatus { get; set; }

        public override string ToString()
        {
            return $"UserPhone: {UserPhoneNumber} \n" +
                   $"UserName: {UserName} \n" +
                   $"OrderStatus: {OrderStatus}";
        }
    }

    public class OrderedProducts
    {
        public MedicalProducts Product { get; set; }

        public int Amount { get; set; }

        public override string ToString()
        {
            return $"Product: {Product.ToString()} \n" +
                   $"Amount: {Amount}";
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