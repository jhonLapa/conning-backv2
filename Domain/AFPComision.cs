namespace Domain
{
    public class AFPComision
    {
        public int IdComision { get; set; }
        public int IdAFP { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal ComisionFija { get; set; }
        public decimal ComisionFlujo { get; set; }
        public decimal ComisionSaldo { get; set; }
        public decimal PrimaSeguros { get; set; }
        public decimal AporteObligatorio { get; set; }
        public decimal RemuneracionAsegurable { get; set; }
        public bool Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // 🔗 Relación
        public AFP AFP { get; set; } = null!;
    }
}
