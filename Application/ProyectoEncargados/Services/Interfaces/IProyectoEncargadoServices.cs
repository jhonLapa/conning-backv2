using Application.Core.Services.Interfaces;
using Application.ProyectoEncargados.Dto;
using Domain;

namespace Application.ProyectoEncargados.Services.Interfaces
{
    public interface IProyectoEncargadoServices : ICrudCoreService<ProyectoEncargadoDto, ProyectoEncargadoSaveDto, int>
    {
        Task<IReadOnlyList<ProyectoEncargadoSelectDto>> SelectActivo();
        Task<PaginadoResponse<ProyectoEncargadoDto>> BusquedaPaginado(PaginationRequest dto);
    }
}
