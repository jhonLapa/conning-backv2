using Application.Ventas.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.Ventas.Services.Interfaces
{
    public interface IVentaServices : ICrudCoreService<VentaDto, VentaSaveDto, int>
    {
        Task<IReadOnlyList<VentaSelectDto>> SelectActivo();
        Task<PaginadoResponse<VentaDto>> BusquedaPaginado(PaginationRequest dto, bool descargarTodo = false, string fechaIni = null, string fechaFin = null);
        Task<OperationResult<List<VentaDto>>> FindByClienteIdAsync(int clienteId);

        Task<OperationResult<VentaDto>> CreateWithDetailsAsync(VentaCompletoSaveDto saveDto);
    }
}