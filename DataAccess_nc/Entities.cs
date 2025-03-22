using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer
{

    public class Users
    {
        [Key]
        public int Id { get; set; }

        public string PhoneNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool Role { get; set; }

        public ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            return $@"id: {Id}
                      PhoneNumber: {PhoneNumber}
                      UserName: {UserName}
                      ";
        }
    }

    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int StatusId { get; set; }

        public int ? DeliveryManId { get; set; } // one time

        [Column("Amount")]
        public int ProductAmount { get; set; }

        public decimal DeliveryPrice { get; set; } // one time

        public DateTime OrderDate { get; set; } // one time

        [ForeignKey(nameof(UserId))]
        public Users User { get; set; } = null!; // one time

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [ForeignKey(nameof(DeliveryManId))]
        public Delivery? DeliveryMan { get; set; }

        [ForeignKey(nameof(StatusId))] 
        public OrderStatus OrderStatus { get; set; } = null!;



        public override string ToString()
        {
            // Convert the list of products to a string
           
            // Build the final string representation
            return
                   $"User: {User?.ToString() ?? "No User"}\n" +
                   $"Product: [{Product?.ToString() ?? "No Product"}]\n" +
                   $"DeliveryManID: {DeliveryManId}\n" +
                   $"DeliveryMan: {DeliveryMan?.ToString() ?? "No delivery Assigned"}\n" +
                   $"DeliveryPrice: {DeliveryPrice}\n" +
                   $"TotalPrice: {TotalPrice}\n" +
                   $"OrderDate: {OrderDate}\n" +
                   $"Status: {OrderStatus?.Status ?? "No status"}";
        }
    }


    [Table("MedicalProducts")]
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;
        
        public decimal Price { get; set; }
        
        public int Stock { get; set; }

        public string ItemDescription { get; set; } = null!;
        
        public int ProductCategory { get; set; }
        
        public int ProductSubCategory { get; set; }

        public override string ToString()
        {
            return $"Product ID: {Id}, Name: {ProductName}, Price: {Price}, Stock: {Stock}, Description: {ItemDescription}, Category: {ProductCategory}, SubCategory: {ProductSubCategory}";
        }

        [ForeignKey(nameof(ProductCategory))] 
        public MedicalCategory MedicalCategory { get; set; } = null!;

        [ForeignKey(nameof(ProductSubCategory))]
        public SubMedicalCategory SubMedicalCategory { get; set; } = null!;
    }

    [Table("Delivery")]
    public class Delivery
    {
        [Key]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }
        
        public string DeliveryManName { get; set; }

        public override string ToString()
        {
            return $"PhoneNumber: {PhoneNumber} \n" +
                   $"DeliveryManName: {DeliveryManName} \n";
        }
    }

    [Table("MedicalCategory")]
    public class MedicalCategory
    {
        public int Id { get; private set; } = -1;

        public string CategoryName { get; set; }

        public ICollection<SubMedicalCategory> SubMedicalCategories { get; set; }
    }

    [Table("SubMedicalCategory")]
    public class SubMedicalCategory
    {
        public int Id { get; private set; } = -1;
        
        public int MainCategory { get; set; }

        public string SubMedicalCategoryName { get; set; }


        [ForeignKey(nameof(MedicalCategory))]
        public MedicalCategory MainMedicalCategory { get; set; }
    }

    public class OrderStatus
    {
        public int Id { get; set; }

        public string Status { get; set; } = null!;
    }
}