using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaHang_DotNet.Models
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ (Ví dụ: abc@gmail.com).")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại phải gồm đúng 10 chữ số.")]
        public string DienThoai { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên.")]
        public string MatKhau { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng xác nhận lại mật khẩu.")]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không trùng khớp.")]
        public string NhapLaiMatKhau { get; set; } = string.Empty;
    }
}