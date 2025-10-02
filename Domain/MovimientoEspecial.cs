namespace Domain
{
    public class MovimientoEspecial
    {
        public int IdMovimientoEspecial { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string TipoMovimiento { get; set; }
        public string CuentaBancaria { get; set; }
        public int Estado { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }
}
