using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Proveedores;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IProveedorService : ICrudCoreService<ProveedorDto, ProveedorSaveDto, int>
    {
        Task<PaginadoResponse<ProveedorDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<ProveedorSelectDto>> SelectActivo();

    }
}