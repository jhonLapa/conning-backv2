using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AportesSindicatos.Dto
{
    public class AportesSindicatoSaveDto
    {
        public int IdProyecto { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public int Estado { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }
}
