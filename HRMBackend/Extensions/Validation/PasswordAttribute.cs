using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace HRMBackend.Extensions.Validation
{
    public class PasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value is null)
                    return new ValidationResult("Mật khẩu không được phép để trống");

                Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,32}$");
                if (!validateGuidRegex.IsMatch(value.ToString()))
                    return new ValidationResult("Mật khẩu không đúng định dạng");

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("Mật khẩu không hợp lệ");
            }
        }
    }
}
