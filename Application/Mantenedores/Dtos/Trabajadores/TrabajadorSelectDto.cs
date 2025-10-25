using Application.Mantenedores.Dtos.Categorias;

namespace Application.Mantenedores.Dtos.Trabajadores
{
    public class TrabajadorSelectDto
    {
        public int IdTrabajador { get; set; }
        public string NumeroDocumento { get; set; }
        public string ApellidosNombres { get; set; }
        public CategoriaDto? Categoria { get; set; }

    }
}
