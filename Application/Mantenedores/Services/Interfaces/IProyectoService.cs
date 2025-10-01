using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Proyectos;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IProyectoService : ICrudCoreService<ProyectoDto , ProyectoSaveDto , int>
    {
    }
}
