namespace Application.Compras.Dto
{
    public class CompraSelectDto
    {
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; } = null!;
        public string Numero { get; set; } = null!;
    }
}
