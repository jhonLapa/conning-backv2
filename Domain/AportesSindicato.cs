using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AportesSindicato
    {
        public int IdAporteSindicato {  get; set; }
        public int IdProyecto { get; set; }
        public string? Mes {  get; set; }
        public decimal Monto { get; set;}
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; }
        public int Estado { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // 🔗 Relaciones
        public Proyecto Proyecto { get; set; } = null!;
    }
}
