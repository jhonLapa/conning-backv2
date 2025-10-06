
using Application.Exceptions;
using Application.Mantenedores.Dtos.TiposComprobantes;
using Application.Mantenedores.Dtos.TiposDocumento;
using Application.Mantenedores.Dtos.Trabajadores;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

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

        public async Task<PaginadoResponse<TrabajadorDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _trabajadorRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<TrabajadorDto>>(response.Data);

            return new PaginadoResponse<TrabajadorDto>(data, response.Meta);
        }
        public async Task<OperationResult<TrabajadorDto>> CreateAsync(TrabajadorSaveDto saveDto)
        {
            var trabajador = _mapper.Map<Trabajador>(saveDto);
            trabajador.FechaCreacion = DateTime.Now;
            trabajador.Estado = 1;

            await _trabajadorRepositorio.SaveAsync(trabajador);

            return new OperationResult<TrabajadorDto>()
            {
                Data = _mapper.Map<TrabajadorDto>(trabajador),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<TrabajadorDto>> DisabledAsync(int id)
        {
            var trabajador = await _trabajadorRepositorio.FindByIdAsync(id);

            if (trabajador == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            trabajador.Estado = trabajador.Estado == 1 ? 0 : 1;
            trabajador.FechaCreacion = DateTime.Now;

            await _trabajadorRepositorio.SaveAsync(trabajador);

            return new OperationResult<TrabajadorDto>()
            {
                Data = _mapper.Map<TrabajadorDto>(trabajador),
                Message = trabajador.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<TrabajadorDto>> EditAsync(int id, TrabajadorSaveDto saveDto)
        {
            var trabajador = await _trabajadorRepositorio.FindByIdAsync(id);

            if (trabajador == null) throw new NotFoundCoreException("Registro no encontrado con ese id");


            _mapper.Map(saveDto, trabajador);

            await _trabajadorRepositorio.SaveAsync(trabajador);

            return new OperationResult<TrabajadorDto>()
            {
                Data = _mapper.Map<TrabajadorDto>(trabajador),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<TrabajadorDto>> FindAllAsync()
        {
            var response = await _trabajadorRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TrabajadorDto>>(response);
        }

        public async Task<TrabajadorDto> FindByIdAsync(int id)
        {
            var trabajador = await _trabajadorRepositorio.FindByIdAsync(id);

            if (trabajador == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<TrabajadorDto>(trabajador);
        }

        public async Task<IReadOnlyList<TrabajadorSelectDto>> SelectActivo()
        {
            var response = await _trabajadorRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<TrabajadorSelectDto>>(response);
        }
    }
}
