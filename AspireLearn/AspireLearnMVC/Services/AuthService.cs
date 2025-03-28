using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AspireLearnMVC.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ✅ Register a new user
        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7285/api/auth/register", model);
            return response.IsSuccessStatusCode;
        }

        // ✅ Login user and return authentication response
        public async Task<AuthResponse> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7285/api/auth/login", model);

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }

        // ✅ Logout user (Optional: Can be used if needed)
        public void Logout()
        {
            // Implement logout logic if necessary (e.g., clearing session data)
        }
    }

    // ✅ Models for Authentication
    public class RegisterModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
