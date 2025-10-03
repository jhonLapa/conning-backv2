using Application.Compras.Dto;
using Application.Core.Services.Interfaces;
using Domain;

namespace Application.Compras.Services.Interfaces
{
    public interface ICompraService : ICrudCoreService<CompraDto, CompraSaveDto, int>
    {
        Task<PaginadoResponse<CompraDto>> BusquedaPaginado(PaginationRequest dto);
    }
}