namespace Domain
{
    public class TipoComprobante : BaseCore
    {
        public int IdTipoComprobante { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
