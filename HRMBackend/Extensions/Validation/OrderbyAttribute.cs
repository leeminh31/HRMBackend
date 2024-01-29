using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HRMBackend.Extensions.Validation
{
    public class OrderbyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value is null)
                    return new ValidationResult("Orderby không được phép để trống");

                string pattern = @"^(?:asc|desc)$";
                if (!string.IsNullOrEmpty((string?)value) && !Regex.IsMatch(value.ToString(), pattern, RegexOptions.Compiled))
                    return new ValidationResult("Orderby không đúng định dạng");

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("Orderby không hợp lệ");
            }
        }
    }
}
