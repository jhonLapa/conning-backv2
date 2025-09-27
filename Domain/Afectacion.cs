namespace Domain
{
    public class Afectacion : BaseCore
    {
        public int IdAfectacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int Estado { get; set; }

        
    }
}
