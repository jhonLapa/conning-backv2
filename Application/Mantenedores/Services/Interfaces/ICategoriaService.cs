using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Categorias;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface ICategoriaService : ICrudCoreService<CategoriaDto, CategoriaSaveDto, int>
    {
        Task<PaginadoResponse<CategoriaDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<CategoriaSelectDto>> SelectActivo();
    }
}