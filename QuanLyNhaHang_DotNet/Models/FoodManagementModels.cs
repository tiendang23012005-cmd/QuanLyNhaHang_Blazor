using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.Frontend.Models
{
    // Model đại diện cho cấu trúc dữ liệu hiển thị trên Grid
    public class FoodViewModel
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; } = string.Empty;
        public string LoaiMon { get; set; } = string.Empty;
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; } = string.Empty;
        public bool TrangThai { get; set; }
        public string MoTa { get; set; } = string.Empty;
    }

    // Model dùng để map trực tiếp dữ liệu thô nhận về từ API GET /api/FoodManagement
    public class FoodApiResponse
    {
        public int MaMonAn { get; set; }
        public string TenMonAn { get; set; } = string.Empty;
        public string DanhMuc { get; set; } = string.Empty;
        public int MaDanhMuc { get; set; }
        public decimal Gia { get; set; }
        public string? MoTa { get; set; }
        public string? HinhAnh { get; set; }
        public bool ConBan { get; set; }
    }

    // Model ràng buộc cho biểu mẫu (EditForm) dùng chung cho Thêm & Sửa
    public class FoodFormModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên món ăn.")]
        [StringLength(100, ErrorMessage = "Tên món ăn không được vượt quá 100 ký tự.")]
        public string TenMonAn { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn danh mục món ăn.")]
        public int MaDanhMuc { get; set; } = 1; 

        [Required(ErrorMessage = "Vui lòng nhập giá.")]
        [Range(1000, 100000000, ErrorMessage = "Giá món ăn phải lớn hơn 1,000 đ.")]
        public decimal Gia { get; set; }

        public string MoTa { get; set; } = string.Empty;

        public string HinhAnh { get; set; } = string.Empty;

        public bool ConBan { get; set; } = true;
    }
}