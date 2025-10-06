using System.Text.Json.Serialization;

namespace Domain
{
    public class Proyecto
    {
        public int IdProyecto { get; set; }
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }   // 👌 nullable
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Estado { get; set; }
        public string FrecuenciaPago { get; set; } = null!; // 👈 En DB es NOT NULL
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        // 🔗 Relaciones
        [JsonIgnore]   // evita el ciclo
        public Cliente Cliente { get; set; } = null!;
        [JsonIgnore]
        public ICollection<TrabajadorProyecto> TrabajadoresProyectos { get; set; } = new List<TrabajadorProyecto>();
        [JsonIgnore]
        public ICollection<ProyectoEncargado> proyectoEncargados { get; set; } = new List<ProyectoEncargado>();
    }
}
