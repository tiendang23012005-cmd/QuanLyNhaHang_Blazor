using QuanLyNhaHang_DotNet.Models;
using System.Net.Http.Json;

namespace QuanLyNhaHang_DotNet.Services;

public class NhanVienService
{
    private readonly HttpClient _http;
    private const string Api = "api/nhanvien";

    public NhanVienService(HttpClient http) => _http = http;

    public Task<NhanVienApiResponse<List<BanAnDto>>?> GetDanhSachBanAsync()
        => _http.GetFromJsonAsync<NhanVienApiResponse<List<BanAnDto>>>($"{Api}/ban-an");

    public Task<NhanVienApiResponse<List<DonHangNhanVienDto>>?> GetDonHangMoiAsync()
        => _http.GetFromJsonAsync<NhanVienApiResponse<List<DonHangNhanVienDto>>>($"{Api}/don-hang-moi");

    public async Task<DonHangHienTaiDto?> GetDonHangTheoBanAsync(int maBan)
    {
        try
        {
            return await _http.GetFromJsonAsync<DonHangHienTaiDto>($"{Api}/ban/{maBan}/don-hang");
        }
        catch
        {
            return null; // 404 = bàn trống, giao diện sẽ hiện "Tạo hóa đơn"
        }
    }

    public Task<HttpResponseMessage> CapNhatSoLuongMonAsync(int maChiTiet, int soLuongMoi)
        => _http.PutAsJsonAsync($"{Api}/chi-tiet/{maChiTiet}/so-luong", new { soLuongMoi });

    public Task<HttpResponseMessage> ThemMonVaoDonHienTaiAsync(int maDonHang, int maMonAn, int soLuong, int? maBan = null)
        => _http.PostAsJsonAsync($"{Api}/don-hang/them-mon", new { maDonHang, maMonAn, soLuong, maBan });

    public Task<HttpResponseMessage> XacNhanThanhToanAsync(int maDonHang)
        => _http.PutAsJsonAsync($"{Api}/don-hang/{maDonHang}/thanh-toan", new { });

    public Task<HttpResponseMessage> HuyDonHangAsync(int maDonHang)
        => _http.PutAsJsonAsync($"{Api}/don-hang/{maDonHang}/huy", new { });
}
