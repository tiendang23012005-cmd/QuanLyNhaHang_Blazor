using QuanLyNhaHang_DotNet.Models;
using System.Net.Http.Json;

namespace QuanLyNhaHang_DotNet.Services;

public class NhanVienService
{
    private readonly HttpClient _http;
    private const string ApiUrl = "https://localhost:7043/api/nhanvien";

    public NhanVienService(HttpClient http)
    {
        _http = http;
    }

    // 1. Lấy sơ đồ bàn tại quán (Bọc qua NhanVienApiResponse)
    public async Task<NhanVienApiResponse<List<BanAnDto>>?> GetDanhSachBanAsync()
    {
        return await _http.GetFromJsonAsync<NhanVienApiResponse<List<BanAnDto>>>($"{ApiUrl}/ban-an");
    }

    // 2. Xem danh sách hóa đơn mới (Bọc qua NhanVienApiResponse)
    public async Task<NhanVienApiResponse<List<DonHangNhanVienDto>>?> GetDonHangMoiAsync()
    {
        return await _http.GetFromJsonAsync<NhanVienApiResponse<List<DonHangNhanVienDto>>>($"{ApiUrl}/don-hang-moi");
    }

    // 3. Chi tiết hóa đơn hiện tại theo mã bàn (TRẢ VỀ TRỰC TIẾP DTO - KHÔNG BỌC)
    public async Task<DonHangHienTaiDto?> GetDonHangTheoBanAsync(int maBan)
    {
        try
        {
            return await _http.GetFromJsonAsync<DonHangHienTaiDto>($"{ApiUrl}/ban/{maBan}/don-hang");
        }
        catch (Exception)
        {
            // Trả về null nếu bàn trống hoặc xảy ra lỗi (404) để giao diện render trạng thái "Tạo hóa đơn"
            return null;
        }
    }

    // 4. Tăng/giảm hoặc xóa món trực tiếp (Dùng PUT)
    public async Task<HttpResponseMessage> CapNhatSoLuongMonAsync(int maChiTiet, int soLuongMoi)
    {
        return await _http.PutAsJsonAsync($"{ApiUrl}/chi-tiet/{maChiTiet}/so-luong", new { soLuongMoi });
    }

    // 5. Thêm món mới vào hóa đơn (Dùng POST)
    public async Task<HttpResponseMessage> ThemMonVaoDonHienTaiAsync(int maDonHang, int maMonAn, int soLuong, int? maBan = null)
    {
        return await _http.PostAsJsonAsync($"{ApiUrl}/don-hang/them-mon", new { maDonHang, maMonAn, soLuong, maBan });
    }

    // 6. Thanh toán hóa đơn và reset trạng thái bàn (Dùng PUT)
    public async Task<HttpResponseMessage> XacNhanThanhToanAsync(int maDonHang)
    {
        return await _http.PutAsJsonAsync($"{ApiUrl}/don-hang/{maDonHang}/thanh-toan", new { });
    }

    // 7. Hủy đơn hàng (Dùng PUT)
    public async Task<HttpResponseMessage> HuyDonHangAsync(int maDonHang)
    {
        return await _http.PutAsJsonAsync($"{ApiUrl}/don-hang/{maDonHang}/huy", new { });
    }
}