namespace QuanLyNhaHang_DotNet.Models
{
    public class RegisterRequest
    {
        public string HoTen { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DienThoai { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
    }

    public class LoginRequest
    {
        public string Identifer { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
    }

    public class AuthResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public string? Token { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
    }
}
