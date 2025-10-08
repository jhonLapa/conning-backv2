using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Asistencias.Dto
{
    public class AsistenciaSelectDto
    {
        public int IdDetallePlanilla { get; set; }
        public string Tipo { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
