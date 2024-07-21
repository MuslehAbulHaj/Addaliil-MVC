using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Addaliil_MVC.Attributes;
namespace Addaliil_MVC.Models
{
    public class Shop
    {
        public long Id { get; set; }
        [DisplayName("Shop URL Name")]
        [ShopNameValidation]
        [Required(ErrorMessage = "Shop URL name is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Shop name must be between 3 and 20 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Display name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Shop name must be between 3 and 100 characters.")]
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        public int? ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public List<Product>? Products { get; set; }

    }
}
