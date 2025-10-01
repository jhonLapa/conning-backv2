namespace Domain
{
    public class RegimenPrevisional
    {
        public int IdRegimen { get; set; }

        public string? Nombre { get; set; }
        public string? Tipo { get; set; }

        // Como en ONP vienen NULL → deben ser nullable
        public decimal? Comision { get; set; }
        public decimal? Prima { get; set; }
        public decimal Aporte { get; set; }   // siempre tiene valor (10/13 en tus datos)
        public decimal Total { get; set; }    // siempre tiene valor (13, 12.84, etc.)
        public decimal? Tope { get; set; }

        public int Estado { get; set; }

        // Relaciones
        public ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();
    }
}
