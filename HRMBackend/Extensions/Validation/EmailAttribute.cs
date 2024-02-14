using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HRMBackend.Extensions.Validation
{
    public class EmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value is null)
                    return new ValidationResult("Email không được phép để trống");

                string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                if (!Regex.IsMatch(value.ToString(), pattern, RegexOptions.Compiled))
                    return new ValidationResult("Email không đúng định dạng");

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("Email không hợp lệ");
            }
        }
    }
}
