using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Dto
{
    public class LoginDto
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public UserView? User { get; set; }
    }


    public class UserView
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Type { get; set; }
        public int? Grade { get; set; }
        public string? Rol { get; set; }

    }

}
