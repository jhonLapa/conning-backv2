namespace Domain
{
    public class Estado
    {
        public int IdEstado { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
