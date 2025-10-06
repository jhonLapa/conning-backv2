using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProyectoEncargados.Dto
{
    public class ProyectoEncargadoSelectDto
    {
        public int IdProyecto { get; set; }
        public int IdTrabajador { get; set; }
        public string rol { get; set; }
    }
}
