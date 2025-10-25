using System.Text.Json.Serialization;

namespace Domain
{
    public class Trabajador
    {
        public int IdTrabajador { get; set; }

        // 🔹 Claves foráneas explícitas
        public int IdCategoria { get; set; }
        public int IdRegimen { get; set; }
        public int TipoDocumentoId { get; set; }

        // 🔹 Datos personales
        public string? NumeroDocumento { get; set; } = null!;
        public string ApellidosNombres { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Sexo { get; set; }
        public string? EstadoCivil { get; set; }
        public int Estado { get; set; }
        public string? Direccion { get; set; }

        // 🔹 Campos numéricos
        public decimal? AsignacionFamiliar { get; set; }
        public int? Hijos { get; set; }

        // 🔹 Auditoría
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        // ============================================
        // 🔗 Relaciones principales
        // ============================================

        public Categoria? Categoria { get; set; }
        public RegimenPrevisional? Regimen { get; set; }
        public TipoDocumento? TipoDocumento { get; set; }
        public ICollection<ProyectoEncargado> proyectoEncargados { get; set; } = new List<ProyectoEncargado>();

        // ✅ Relaciones 1:N
        public ICollection<CuentaBancariaTrabajador> CuentasBancarias { get; set; } = new List<CuentaBancariaTrabajador>();
        public ICollection<TrabajadorProyecto> TrabajosProyectos { get; set; } = new List<TrabajadorProyecto>();
    }
}
