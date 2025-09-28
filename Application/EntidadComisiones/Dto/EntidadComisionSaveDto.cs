namespace Application.EntidadComisiones.Dto
{
    public class EntidadComisionSaveDto
    {
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

    }
}
