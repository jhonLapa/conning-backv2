using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Menus;
using Domain;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IMenuService : ICrudCoreService<MenuDto, MenuSaveDto, int>
    {
        Task<PaginadoResponse<MenuDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<MenuSelectDto>> SelectActivo();

    }
}