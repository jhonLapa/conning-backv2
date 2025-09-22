using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConfigAfectacions.Dto
{
    public class ConfigAfectacionSaveDto
    {
        public int IdEmpresa { get; set; }
        public int IdAfectacion { get; set; }
        public decimal Porcentaje { get; set; }
        public bool Activo { get; set; }
    }
}
