using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Frontend.Models
{
    public class UserModel
    {
        public int MaNguoiDung { get; set; } // Tương đương UserId từ API

        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá {1} ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng.")]
        public string DienThoai { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn vai trò.")]
        public int MaVaiTro { get; set; }

        public bool TrangThaiHoatDong { get; set; }
    }
}