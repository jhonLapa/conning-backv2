using Application.DetalleVentas.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.DetalleVentas.Services.Interfaces
{
    public interface IDetalleVentaServices : ICrudCoreService<DetalleVentaDto, DetalleVentaSaveDto, int>
    {
        Task<OperationResult<List<DetalleVentaDto>>> ObtenerPorVentaAsync(int id);
        Task<PaginadoResponse<DetalleVentaDto>> BusquedaPaginado(PaginationRequest dto);
    }
}
