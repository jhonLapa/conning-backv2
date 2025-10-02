namespace Application.Mantenedores.Dtos.TiposComprobantes
{
    public class TipoComprobanteSelectDto
    {
        public int IdTipoComprobante { get; set; }
        public string Codigo { get; set; }      // hasta 10
        public string Nombre { get; set; }      // hasta 100
    }
}
