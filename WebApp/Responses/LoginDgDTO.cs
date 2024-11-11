using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApp.DTOs
{
    public class LoginDgDTO
    {
        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng điền đầy đủ thông tin")]
        public required string Sdt { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng điền đầy đủ thông tin")]
        public string? PasswordDg { get; set; }

        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Vui lòng điền đầy đủ thông tin")]
        public string? HoTen { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Vui lòng điền đầy đủ thông tin")]
        [EmailAddress(ErrorMessage = "Vui lòng điền đúng định dạng Email")]
        public string? Email { get; set; }
    }
}
