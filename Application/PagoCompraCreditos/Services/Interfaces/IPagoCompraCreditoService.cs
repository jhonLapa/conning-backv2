using Application.PagoCompraCreditos.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.PagoCompraCreditos.Services.Interfaces
{
    public interface IPagoCompraCreditoServices : ICrudCoreService<PagoCompraCreditoDto, PagoCompraCreditoSaveDto, int>
    {
        Task<OperationResult<List<PagoCompraCreditoDto>>> ObtenerPorCompraAsync(int id);
        Task<PaginadoResponse<PagoCompraCreditoDto>> BusquedaPaginado(PaginationRequest dto);
    }
}