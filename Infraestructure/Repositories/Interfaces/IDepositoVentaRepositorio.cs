using Domain;
using Domain.Entities;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IDepositoVentaRepositorio : ICrudCoreRespository<DepositoVenta, int>
    {
        Task<PaginadoResponse<DepositoVenta>> BusquedaPaginado(PaginationRequest dto);
        Task<List<DepositoVenta>> ObtenerPorVentaAsync(int idVenta);
        Task DeleteByVentaIdAsync(int idVenta);


    }
}