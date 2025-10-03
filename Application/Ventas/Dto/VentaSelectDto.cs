namespace Application.Ventas.Dto
{
    public class VentaSelectDto
    {
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; } = null!;
        public string Numero { get; set; } = null!;
    }
}
