using Application.Usuarios.Dto;

namespace Application.Auth.Dto
{
    public class UserSecurityDto 
    {
        public bool Success { get; set; }
        public LoginDto? Data { get; set; }
        public string Message { get; set; }
    }
}
