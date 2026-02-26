using Application.Departamentos.Dto;

namespace Application.Departamentos.Services.Interface
{
    public interface IDepartamentoService
    {
        Task<List<DepartamentoDto>> GetAllAsync();
        Task<DepartamentoDto?> GetByIdAsync(int id);
        Task<DepartamentoDto> SaveAsync(SaveDepartamento dto);
        Task<bool> DeleteAsync(int id);
    }
}