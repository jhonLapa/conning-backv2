using Application.Compras.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.Compras.Services.Interfaces
{
    public interface ICompraServices : ICrudCoreService<CompraDto, CompraSaveDto, int>
    {
        Task<IReadOnlyList<CompraSelectDto>> SelectActivo();
        Task<PaginadoResponse<CompraDto>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null);
        Task<OperationResult<List<CompraDto>>> FindByProveedorIdAsync(int proveedorId);

        Task<OperationResult<CompraDto>> CreateWithDetailsAsync(CompraCompletoSaveDto saveDto);
    }
}