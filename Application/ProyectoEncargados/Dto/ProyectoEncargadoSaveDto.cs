using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProyectoEncargados.Dto
{
    public class ProyectoEncargadoSaveDto
    {
        public int IdProyecto { get; set; }
        public int IdTrabajador { get; set; }
        public string Rol { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        
    }
}
