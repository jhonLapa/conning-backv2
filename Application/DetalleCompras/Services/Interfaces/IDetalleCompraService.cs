using Application.DetalleCompras.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.DetalleCompras.Services.Interfaces
{
    public interface IDetalleCompraServices : ICrudCoreService<DetalleCompraDto, DetalleCompraSaveDto, int>
    {
        Task<OperationResult<List<DetalleCompraDto>>> ObtenerPorCompraAsync(int id);
        Task<PaginadoResponse<DetalleCompraDto>> BusquedaPaginado(PaginationRequest dto);
    }
}
