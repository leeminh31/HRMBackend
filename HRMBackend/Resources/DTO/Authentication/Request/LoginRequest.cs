using HRMBackend.Extensions.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRMBackend.Resources.DTO.Authentication.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Dữ liệu tên đăng nhập là bắt buộc!")]
        [MaxLength(50, ErrorMessage = "Độ dài tối đa 50 ký tự")]
        [JsonPropertyName("tenDangNhap")]
        public string TenDangNhap { get; set; }
        [JsonPropertyName("matKhau")]
        [Required(ErrorMessage = "Dữ liệu mật khẩu là bắt buộc!")]
        public string MatKhau { get; set; }
    }
}
