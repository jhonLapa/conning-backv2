using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Clientes;
using Application.Mantenedores.Dtos.Trabajadores;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface ITrabajadorService : ICrudCoreService<TrabajadorDto, TrabajadorSaveDto,int>
    {
        Task<PaginadoResponse<TrabajadorDto>> BusquedaPaginado(PaginationRequest dto);
        Task<IReadOnlyList<TrabajadorSelectDto>> SelectActivo();
    }
}
