using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ConceptosCategoria
    {
        public int IdConcepto { get; set; }
        public int IdCategoria { get; set; }
        public string NombreConcepto { get; set; } 
        public decimal Valor { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCambioEstado { get; set; }
        public string? UsuarioCambioEstado { get; set; }

        // Relaciones
        public Categoria Categoria { get; set; } = null!;
    }
}
