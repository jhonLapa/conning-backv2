using Application.DetalleVentas.Dto;
using Domain;

namespace Application.Mantenedores.Dtos.Categorias
{
    public class CategoriaDto
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        public List<ConceptosCategoria> ConceptosCategoria { get; set; } = new();

    }
}
