using Application.DetalleCompras.Dto;
using Domain;

namespace Application.DetalleCompras.Services.Interfaces
{
    public interface IDetalleCompraService
    {
        Task<IReadOnlyList<DetalleCompraDto>> FindAllAsync();
        Task<DetalleCompraDto> FindByIdAsync(int id);
        Task<OperationResult<DetalleCompraDto>> CreateAsync(DetalleCompraSaveDto saveDto);
        Task<OperationResult<DetalleCompraDto>> EditAsync(int id, DetalleCompraSaveDto saveDto);
        Task<PaginadoResponse<DetalleCompraDto>> BusquedaPaginado(PaginationRequest dto);
    }
}