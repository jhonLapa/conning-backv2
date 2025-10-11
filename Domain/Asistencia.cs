using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int IdDetallePlanilla { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public int HorasTrabajadas { get; set; }
        public string? Observacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

    }
}
