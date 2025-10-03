using Application.PagoVentaCreditos.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.PagoVentaCreditos.Services.Interfaces
{
    public interface IPagoVentaCreditoServices : ICrudCoreService<PagoVentaCreditoDto, PagoVentaCreditoSaveDto, int>
    {
        Task<OperationResult<List<PagoVentaCreditoDto>>> ObtenerPorVentaAsync(int id);
        Task<PaginadoResponse<PagoVentaCreditoDto>> BusquedaPaginado(PaginationRequest dto);
    }
}