using System.Text.Json.Serialization;

namespace Domain
{
    public class TipoDocumento
    {
        public int IdTipoDocumento { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        
        [JsonIgnore]   
        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        [JsonIgnore]
        public ICollection<Proveedor> Proveedores { get; set; } = new List<Proveedor>();
        public ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();
    }
}
