using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Categorias;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface ICategoriaService : ICrudCoreService<CategoriaDto, CategoriaSaveDto, int>
    {
    }
}