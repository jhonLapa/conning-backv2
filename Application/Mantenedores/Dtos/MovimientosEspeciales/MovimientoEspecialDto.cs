namespace Application.Mantenedores.Dtos.MovimientosEspeciales
{
    public class MovimientoEspecialDto
    {
        public int IdMovimientoEspecial { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = null!;
        public float Monto { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public string CuentaBancaria { get; set; } = null!;
        public int Estado { get; set; }
        public string Observacion { get; set; } = null!;
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }


    }
}
