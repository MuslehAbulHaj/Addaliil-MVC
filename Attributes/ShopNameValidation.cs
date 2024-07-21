using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Addaliil_MVC.Attributes
{
    public class ShopNameValidation : ValidationAttribute
    {

        private readonly List<string> _reservedWords = new List<string>
        {
            "shop",
            "product",
            "admin",
            "category"
            // Add other reserved words here
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var shopName = value.ToString();
                // Regex to allow only letters, numbers, and underscores
                var regex = new Regex(@"^[a-zA-Z0-9_]+$");
                if (!regex.IsMatch(shopName))
                {
                    return new ValidationResult("shopName can only contain letters, numbers, and underscores.");
                }

                // Check for reserved words (case insensitive)
                if (_reservedWords.Any(word => string.Equals(word, shopName, StringComparison.OrdinalIgnoreCase)))
                {
                    return new ValidationResult($"The shopName '{shopName}' is not allowed.");
                }
            }
            return ValidationResult.Success;
        }

    }
}
