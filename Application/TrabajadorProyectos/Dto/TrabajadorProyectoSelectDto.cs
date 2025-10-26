namespace Application.TrabajadorProyectos.Dto
{
    public class TrabajadorProyectoSelectDto
    {
        public int IdTrabajador { get; set; }
        public int IdProyecto { get; set; }
        public int Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
    }
}
