namespace Application.Conceptos.Dto
{
    public class ConceptoSaveDto
    {
        public int IdGrupo { get; set; }
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Activo { get; set; }
        public string? CtaDebe { get; set; }
        public string? CtaHaber { get; set; }
        public bool PrincipalID { get; set; }
        public bool CalculoAutomatico { get; set; }
        public bool GeneraArchivoPLAME { get; set; }
 
    }
}
