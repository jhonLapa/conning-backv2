using Application.Mantenedores.Dtos.Clientes;
using Application.Mantenedores.Dtos.Trabajadores;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Services
{
    public class TrabajadorService : ITrabajadorService
    {
        private readonly ITrabajadorRepositorio _trabajadorRepositorio;
        private readonly IMapper _mapper;
        public TrabajadorService(ITrabajadorRepositorio TrabajadorRepositorio, IMapper mapper)
        {
            _trabajadorRepositorio = TrabajadorRepositorio;
            _mapper = mapper;
        }
        public Task<PaginadoResponse<TrabajadorDto>> BusquedaPaginado(PaginationRequest dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<TrabajadorDto>> CreateAsync(TrabajadorSaveDto saveDto)
        {
            var trabajador = _mapper.Map<Trabajador>(saveDto);
            trabajador.FechaCreacion = DateTime.Now;
            trabajador.Activo = 1;

            await _trabajadorRepositorio.SaveAsync(trabajador);

            return new OperationResult<TrabajadorDto>()
            {
                Data = _mapper.Map<TrabajadorDto>(trabajador),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public Task<OperationResult<TrabajadorDto>> DisabledAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TrabajadorDto>> EditAsync(int id, TrabajadorSaveDto saveDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<TrabajadorDto>> FindAllAsync()
        {
            var response = await _trabajadorRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TrabajadorDto>>(response);
        }

        public Task<TrabajadorDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TrabajadorSelectDto>> SelectActivo()
        {
            throw new NotImplementedException();
        }
    }
}
