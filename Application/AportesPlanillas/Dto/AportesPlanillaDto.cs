using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AportesPlanillas.Dto
{
    public class AportesPlanillaDto
    {
        public int IdAportesPlanilla { get; set; }
        public int IdPlanilla { get; set; }
        public string TipoAporte { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public int Estado { get; set; }
        public int IdProyecto { get; set; }

        // 🔗 Relaciones
        public Planilla Planilla { get; set; } = null!;
    }
}
