using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using QuanLyNhaHang_DotNet;
using QuanLyNhaHang_DotNet.Infrastructure;
using QuanLyNhaHang_DotNet.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ============================================================================
// 1. Cấu hình phân quyền hệ thống cơ bản (ĐÃ SỬA ĐỂ CHỐNG LỆCH PHA THỰC THỂ)
// ============================================================================
builder.Services.AddAuthorizationCore();

// Đăng ký CustomAuthStateProvider đích danh trước
builder.Services.AddScoped<CustomAuthStateProvider>();

// Ép AuthenticationStateProvider của Microsoft dùng chung thực thể duy nhất ở trên
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthStateProvider>());


// ============================================================================
// 2. Đăng ký bộ lọc Token làm nhiệm vụ Interceptor
// ============================================================================
builder.Services.AddTransient<JwtAuthorizationMessageHandler>();


// ============================================================================
// 3. Cấu hình HttpClient dùng chung (Tự động đính kèm Interceptor)
// ============================================================================
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<JwtAuthorizationMessageHandler>();
    handler.InnerHandler = new HttpClientHandler(); // Gán handler gốc chạy trên trình duyệt

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7043/") // Cổng API Backend của bạn
    };
});


// ============================================================================
// 4. Đăng ký các Service
// ============================================================================
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<NhanVienService>();

await builder.Build().RunAsync();