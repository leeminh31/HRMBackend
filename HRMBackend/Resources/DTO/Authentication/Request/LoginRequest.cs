using HRMBackend.Extensions.Validation;
using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.Authentication.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Dữ liệu tên đăng nhập là bắt buộc!")]
        [MaxLength(50, ErrorMessage = "Độ dài tối đa 50 ký tự")]
        [Email]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Dữ liệu mật khẩu là bắt buộc!")]
        public string MatKhau { get; set; }
    }
}
