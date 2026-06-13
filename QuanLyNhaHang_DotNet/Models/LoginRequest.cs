using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaHang_DotNet.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Tài khoản không được để trống.")]
        public string Identifer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải chứa ít nhất 6 ký tự.")]
        public string MatKhau { get; set; } = string.Empty;
    }
}