namespace Application.Mantenedores.Dtos.RegimenesPrevisionales
{
    public class RegimenPrevisionalSaveDto
    {
        public string? Nombre { get; set; }
        public string? Tipo { get; set; }
        public decimal? Comision { get; set; }
        public decimal? Prima { get; set; }
        public decimal Aporte { get; set; }  
        public decimal Total { get; set; }    
        public decimal? Tope { get; set; }
    }
}
