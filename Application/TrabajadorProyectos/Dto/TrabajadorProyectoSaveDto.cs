using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TrabajadorProyectos.Dto
{
    public class TrabajadorProyectoSaveDto
    {
        public int IdTrabajador { get; set; }
        public int IdProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }
}
