namespace Domain
{
    public class Concepto
    {
        public int IdConcepto { get; set; }
        public int IdGrupo { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Activo { get; set; }
        public string? CtaDebe { get; set; }
        public string? CtaHaber { get; set; }
        public string? PrincipalDH { get; set; }
        public bool CalculoAutomatico { get; set; }
        public bool GeneraArchivoPLAME { get; set; }
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Relaciones
        public GrupoConcepto Grupo { get; set; }
        public ICollection<ConceptoAfectacion> ConceptoAfectaciones { get; set; }

    }
}
