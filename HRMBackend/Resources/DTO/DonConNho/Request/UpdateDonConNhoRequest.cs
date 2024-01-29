using System.ComponentModel.DataAnnotations;

namespace HRMBackend.Resources.DTO.DonConNho.Request
{
    public class UpdateDonConNhoRequest
    {
        [Required(ErrorMessage = "Dữ liệu mã id là bắt buộc!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Dữ liệu ký hiệu là bắt buộc!")]
        [MinLength(3, ErrorMessage = "Độ dài ký hiệu tối thiểu 3 ký tự")]
        [MaxLength(10, ErrorMessage = "Độ dài ký hiệu tối đa 10 ký tự")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Dữ liệu tên cảng là bắt buộc!")]
        [MinLength(5, ErrorMessage = "Độ dài tên cảng tối thiểu 5 ký tự")]
        [MaxLength(50, ErrorMessage = "Độ dài tên cảng tối đa 50 ký tự")]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
        public int Status { get; set; }

        [MaxLength(1000, ErrorMessage = "Độ dài mô tả tối đa 1000 ký tự")]
        public string Description { get; set; }
    }
}
