using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.Authentication.Request
{
    public class LogoutRequest
    {
        [Required(ErrorMessage = "Dữ liệu Id là bắt buộc!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Dữ liệu RefreshToken là bắt buộc!")]
        public string RefreshToken { get; set; }
    }
}
