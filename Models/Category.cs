using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Addaliil_MVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        //public Icon Icon { get; set; }
        
        [Required]
        public int DisplayOrder { get; set; }
    }
}
