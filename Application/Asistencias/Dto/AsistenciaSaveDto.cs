using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Asistencias.Dto
{
    public class AsistenciaSaveDto
    {
        public int IdDetallePlanilla { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public int HorasTrabajadas { get; set; }
        public string? Observacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }
}
