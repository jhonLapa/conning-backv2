using Application.TrabajadorProyectos.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.TrabajadorProyectos.Services.Interfaces
{
    public interface ITrabajadorProyectoServices : ICrudCoreService<TrabajadorProyectoDto, TrabajadorProyectoSaveDto, int>
    {
        Task<IReadOnlyList<TrabajadorProyectoSelectDto>> SelectActivo();
        Task<PaginadoResponse<TrabajadorProyectoDto>> BusquedaPaginado(PaginationRequest dto);
    }
}