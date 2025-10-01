namespace Application.Mantenedores.Dtos.RegimenesPrevisionales
{
    public class RegimenPrevisionalDto
    {
        public int IdRegimen { get; set; }
        public string Nombre { get; set; }
        public decimal Comision { get; set; }
        public decimal Prima { get; set; }
        public decimal Aporte { get; set; }
        public decimal Total { get; set; }
        public decimal Tope { get; set; }
        public int Estado { get; set; }
    }
}
