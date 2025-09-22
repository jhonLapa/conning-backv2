namespace Domain
{
    public class ConfigAfectacion
    {
        public int IdConfigAfectacion { get; set; }
        public int IdEmpresa { get; set; }
        public int IdAfectacion { get; set; }
        public decimal Porcentaje { get; set; }
        public bool Activo { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }

        // 🔗 Relaciones
        public Empresa Empresa { get; set; } = null!;
        public Afectacion Afectacion { get; set; } = null!;
    }
}
