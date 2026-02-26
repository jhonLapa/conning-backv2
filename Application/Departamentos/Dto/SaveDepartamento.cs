using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departamentos.Dto
{
    public class SaveDepartamento
    {
        public int? Id { get; set; } // Para Update
        public string Nombre { get; set; } = string.Empty;
    }
}
