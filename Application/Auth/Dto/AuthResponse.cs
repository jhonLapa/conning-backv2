
using Domain;

namespace Application.Auth.Dto
{
    public class AuthResponse
    {
        public LoginRequest? Login { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }
}
