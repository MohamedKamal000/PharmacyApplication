
using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Dtos.Category_DTOs
{
    public class AddSubCategoryDto
    {
        [Required]
        public string SubCategoryName { get; set; } = null!;

        [Required]
        public int MainCategoryId { get; set; } 


    }
}
