using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Dtos.AportesEmpleadores
{
    public class AportesEmpleadorSaveDto
    {
        public string Nombre { get; set; } = null!;
        public float Tasa { get; set; }
        public float Base { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
