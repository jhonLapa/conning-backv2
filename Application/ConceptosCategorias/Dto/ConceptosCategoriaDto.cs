using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ConceptosCategorias.Dto
{
    public class ConceptosCategoriaDto
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
