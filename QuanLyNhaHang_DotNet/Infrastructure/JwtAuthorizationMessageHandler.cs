using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.JSInterop;
using QuanLyNhaHang_DotNet.Models;

namespace QuanLyNhaHang_DotNet.Infrastructure;

public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly IJSRuntime _jsRuntime;

    public JwtAuthorizationMessageHandler(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            // Dùng IJSRuntime gọi thẳng vào sessionStorage của trình duyệt
            var sessionJson = await _jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", "currentUser");

            if (!string.IsNullOrEmpty(sessionJson))
            {
                var userSession = JsonSerializer.Deserialize<UserSession>(sessionJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (userSession != null && !string.IsNullOrEmpty(userSession.Token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userSession.Token);
                }
            }
        }
        catch
        {
            // Phòng hờ lỗi render hệ thống
        }

        return await base.SendAsync(request, cancellationToken);
    }
}