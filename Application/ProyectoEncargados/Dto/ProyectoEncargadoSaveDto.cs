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
        public string rol { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        
    }
}
