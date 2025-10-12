namespace Application.Usuarios.Dto
{
    public class UserRoleSaveDto 
    {
        public int UserId  { get; set; }
        public int  RoleId { get; set; }
        public bool State { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; } // <--- ¡AÑADIR!
        public string LastName { get; set; }   // <--- ¡AÑADIR!
        public string Email { get; set; }      // <--- ¡AÑADIR!
    }
}
