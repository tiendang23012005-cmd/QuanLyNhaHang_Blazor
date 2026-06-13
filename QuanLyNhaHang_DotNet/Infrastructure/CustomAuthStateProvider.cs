using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using QuanLyNhaHang_DotNet.Models;

namespace QuanLyNhaHang_DotNet.Infrastructure;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var sessionJson = await _jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", "currentUser");
            if (string.IsNullOrWhiteSpace(sessionJson))
            {
                return new AuthenticationState(_anonymous);
            }

            // Thêm tùy chọn PropertyNameCaseInsensitive để mapping mượt mà các thuộc tính viết hoa/thường từ JSON
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var userSession = JsonSerializer.Deserialize<UserSession>(sessionJson, options);

            if (userSession == null || string.IsNullOrWhiteSpace(userSession.Token))
            {
                return new AuthenticationState(_anonymous);
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userSession.FullName ?? "Thành viên"),
            new Claim(ClaimTypes.Role, userSession.Role ?? "Guest"),
            new Claim("Token", userSession.Token)
        };

            var identity = new ClaimsIdentity(claims, "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch
        {
            // Nếu có bất kỳ lỗi phân tích nào, trả về trạng thái Chưa đăng nhập chứ không làm sập ứng dụng
            return new AuthenticationState(_anonymous);
        }
    }

    public void NotifyUserLoggedIn(UserSession userSession)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userSession.FullName),
            new Claim(ClaimTypes.Role, userSession.Role),
            new Claim("Token", userSession.Token)
        };

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}