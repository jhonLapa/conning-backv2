using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Categorys;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface ICategoryService : ICrudCoreService<CategoryDto, CategorySaveDto, int>
    {
    }
}