using Application.Core.Services.Interfaces;
using Application.EntidadComisiones.Dto;

namespace Application.EntidadComisiones.Services.Interfaces
{
    public interface IEntidadComisionServices : ICrudCoreService<EntidadComisionDto, EntidadComisionSaveDto, int>
    {
    }
}