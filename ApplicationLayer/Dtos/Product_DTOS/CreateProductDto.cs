

using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Dtos.Product_DTOS
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Invalid Input")] 
        [StringLength(maximumLength:190,MinimumLength = 20)]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "Invalid Input")] 
        [Range(1,10000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Invalid Input")] 
        [Range(1,10000)]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Invalid Input")] 
        [StringLength(maximumLength:1000,MinimumLength = 0)]
        public string ItemDescription { get; set; } = null!;

        // current : 4
        [Required(ErrorMessage = "Invalid Input")] 
        public int ProductCategory { get; set; }

        // current : 3
        [Required(ErrorMessage = "Invalid Input")] 
        public int ProductSubCategory { get; set; }
        
        /*
         * [Error],[GlobalErrorHandlerMiddleWare]: System error occurred Exception Message:System error occurred
         */

        public override string ToString()
        {
            return $"ProductName:{ProductName}, Price:{Price}, Stock:{Stock}, " +
                   $"ProductCategory:{ProductCategory}, SubCategory:{ProductSubCategory}";
        }
    }
}
