using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Frontend.Models
{
    public class TableManagementModel
    {
        public int MaBan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số bàn (Ví dụ: Bàn 01).")]
        [StringLength(50, ErrorMessage = "Số bàn không được vượt quá 50 ký tự.")]
        public string SoBan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập sức chứa.")]
        [Range(1, 100, ErrorMessage = "Sức chứa phải nằm trong khoảng từ 1 đến 100 người.")]
        public int SucChua { get; set; } = 4;

        [Required(ErrorMessage = "Vui lòng chọn trạng thái bàn.")]
        public string TrangThaiBan { get; set; } = "Trống";
    }
}