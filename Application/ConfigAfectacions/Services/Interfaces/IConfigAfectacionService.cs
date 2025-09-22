using Application.Core.Services.Interfaces;
using Application.ConfigAfectacions.Dto;
using Domain;

namespace Application.ConfigAfectacions.Services.Interfaces
{
    public interface IConfigAfectacionServices : ICrudCoreService<ConfigAfectacionDto, ConfigAfectacionSaveDto, int>
    {
        Task<OperationResult<IEnumerable<ConfigAfectacionDto>>> SaveArrayAsync(IEnumerable<ConfigAfectacionSaveDto> saveDtos);

    }
}
