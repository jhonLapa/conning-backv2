using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AportesPlanillas.Dto
{
    public class AportesPlanillaSelectDto
    {
        public int IdPlanilla { get; set; }
        public string TipoAporte { get; set; }
        public int Estado { get; set; }

    }
}
