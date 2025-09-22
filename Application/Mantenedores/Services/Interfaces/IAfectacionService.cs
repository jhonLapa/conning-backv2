using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Afectacions;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IAfectacionService : ICrudCoreService<AfectacionDto, AfectacionSaveDto, int>
    {
    }
}
