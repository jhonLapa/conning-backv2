using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BaseCore
    {
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
    }
}
