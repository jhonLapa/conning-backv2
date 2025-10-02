namespace Domain
{
    public class AportesEmpleador
    {
        public int IdAporte { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Tasa { get; set; }     // DECIMAL(5,2)
        public decimal? Base { get; set; }    // DECIMAL(10,2) NULL
        public int Estado { get; set; }
    }
}
