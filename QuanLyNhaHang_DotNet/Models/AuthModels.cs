using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaHang_DotNet.Models;

public class AuthResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Token { get; set; }
    public string? FullName { get; set; }
    public string? Role { get; set; }
}

public class UserSession
{
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

// ✅ THÊM 2 CLASS NÀY
public class ForgotPasswordRequest
{
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; } = string.Empty;
}

public class ResetPasswordRequest
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
    [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
    public string MatKhauMoi { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
    [Compare(nameof(MatKhauMoi), ErrorMessage = "Mật khẩu nhập lại không khớp")]
    public string NhapLaiMatKhau { get; set; } = string.Empty;
}