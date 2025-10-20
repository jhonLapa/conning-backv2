using Application.Core.Services.Interfaces;
using Application.Empresas.Dto;
using Domain;

namespace Application.Empresas.Services.Interfaces
{
    public interface IEmpresaServices : ICrudCoreService<EmpresaDto, EmpresaSaveDto, int>
    {
        Task<PaginadoResponse<EmpresaDto>> BusquedaPaginado(PaginationRequest dto);
    }
}