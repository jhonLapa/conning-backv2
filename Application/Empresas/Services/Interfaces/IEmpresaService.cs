using Application.Core.Services.Interfaces;
using Application.Empresas.Dto;

namespace Application.Empresas.Services.Interfaces
{
    public interface IEmpresaServices : ICrudCoreService<EmpresaDto, EmpresaSaveDto, int>
    {
    }
}