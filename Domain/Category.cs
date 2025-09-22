namespace Domain
{
    public class Category
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
 
    }
}
