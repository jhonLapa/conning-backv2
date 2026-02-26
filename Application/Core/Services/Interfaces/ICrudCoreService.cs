using Domain;

namespace Application.Core.Services.Interfaces
{
    public interface ICrudCoreService<TDto, TSaveDto, ID>
    {
        Task<IReadOnlyList<TDto>> FindAllAsync();
        Task<TDto> FindByIdAsync(ID id);
    }
}
