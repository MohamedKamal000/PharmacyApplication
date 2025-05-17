
using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Dtos.Category_DTOs
{
    public class AddSubCategoryDto
    {
        [Required]
        [StringLength(maximumLength:40,MinimumLength = 4,ErrorMessage = "Invalid SubCategoryName")]
        public string SubCategoryName { get; set; } = null!;

        [Required]
        
        public int MainCategoryId { get; set; } 


    }
}
