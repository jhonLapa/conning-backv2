using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConfigAfectacions.Dto
{
    public class ConfigAfectacionDto
    {
        public int IdConfigAfectacion { get; set; }
        public int IdEmpresa { get; set; }
        public int IdAfectacion { get; set; }
        public decimal Porcentaje { get; set; }
        public bool Activo { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }

        // 🔗 Relaciones
        public Domain.Empresa Empresa { get; set; } = null!;
        public Afectacion Afectacion { get; set; } = null!;
    }
}
