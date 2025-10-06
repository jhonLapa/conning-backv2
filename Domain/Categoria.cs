using System.Text.Json.Serialization;

namespace Domain
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        // Relaciones
        [JsonIgnore]
        public ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();

    }
}
