namespace Domain
{
    public class Bank : BaseCore
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? NombreCorto { get; set; }
        public string? SwiftCode { get; set; }
        public string? CodigoPais { get; set; }
        public int Estado { get; set; }
    }
}
