namespace Domain
{
    public class EntidadComision
    {
        public int IdComision { get; set; }
        public int IdEntidad { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }

        public decimal? ComisionFija { get; set; }
        public decimal? ComisionFlujo { get; set; }
        public decimal? ComisionMixtaFlujo { get; set; }
        public decimal? ComisionMixtaSaldo { get; set; }
        public decimal? PrimaSeguro { get; set; }
        public decimal? AporteObligatorio { get; set; }
        public decimal? RemuneracionAsegurable { get; set; }

        public bool Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // 🔗 Relación: cada comisión pertenece a una entidad
        public EntidadPrevisional Entidad { get; set; } = null!;
    }
}
