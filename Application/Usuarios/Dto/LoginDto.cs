namespace Application.Usuarios.Dto
{
    public class LoginDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserView User { get; set; }
        public RolView Rol { get; set; }
    }


    public class UserView
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool State { get; set; }
    }

    public class RolView
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public bool State { get; set; }
    }

}
