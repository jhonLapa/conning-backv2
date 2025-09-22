using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Dtos.Roles
{
    public class RoleSaveDto
    {
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public bool State { get; set; } = true;
    }
}
