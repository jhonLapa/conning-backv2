namespace Application.Mantenedores.Dtos.TiposComprobantes
{
    public class TipoComprobanteDto
    {
        public int IdTipoComprobante  { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }

    }
}
