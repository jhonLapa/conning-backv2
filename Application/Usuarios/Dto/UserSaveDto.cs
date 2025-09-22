using Domain;

namespace Application.Usuarios.Dto
{
    public class UserSaveDto 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool State { get; set; } = true;

    }
}
