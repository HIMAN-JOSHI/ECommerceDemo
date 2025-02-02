using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.Models
{
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name should not exceed more than 50 chars.")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, int.MaxValue, ErrorMessage = "Price should be at least 0.01")]
        public decimal Price { get; set; }
    }
}
