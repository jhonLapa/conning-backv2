using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Dtos.MovimientosEspeciales
{
    public class MovimientoEspecialSaveDto
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string TipoMovimiento { get; set; }
        public string CuentaBancaria { get; set; }
        public string Observacion { get; set; }
    }
}
