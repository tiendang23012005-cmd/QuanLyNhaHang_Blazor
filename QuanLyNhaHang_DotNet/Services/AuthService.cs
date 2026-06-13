using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using QuanLyNhaHang_DotNet.Models;
using QuanLyNhaHang_DotNet.Infrastructure;

namespace QuanLyNhaHang_DotNet.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient http, IJSRuntime jsRuntime, AuthenticationStateProvider authStateProvider)
    {
        _http = http;
        _jsRuntime = jsRuntime;
        _authStateProvider = authStateProvider;
    }

    public async Task<AuthResponse?> RegisterAsync(object user)
    {
        var response = await _http.PostAsJsonAsync("api/auth/register", user);
        return await response.Content.ReadFromJsonAsync<AuthResponse>();
    }

    public async Task<AuthResponse?> LoginAsync(string identifier, string matKhau)
    {
        var loginRequest = new LoginRequest { Identifer = identifier, MatKhau = matKhau };
        var response = await _http.PostAsJsonAsync("api/auth/login", loginRequest);

        var authResult = await response.Content.ReadFromJsonAsync<AuthResponse>();

        if (authResult != null && authResult.IsSuccess && !string.IsNullOrEmpty(authResult.Token))
        {
            var userSession = new UserSession
            {
                FullName = authResult.FullName ?? "",
                Role = authResult.Role ?? "",
                Token = authResult.Token
            };

            var sessionJson = JsonSerializer.Serialize(userSession);
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", sessionJson);

            ((CustomAuthStateProvider)_authStateProvider).NotifyUserLoggedIn(userSession);
        }

        return authResult;
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "currentUser");
        ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
    }
}