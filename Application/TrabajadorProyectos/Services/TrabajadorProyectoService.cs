using Application.Exceptions;
using Application.ProyectoEncargados.Dto;
using Application.TrabajadorProyectos.Dto;
using Application.TrabajadorProyectos.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.TrabajadorProyectos.Servicess
{
    public class TrabajadorProyectoService : ITrabajadorProyectoServices
    {
        private readonly ITrabajadorProyectoRepositorio _trabajadorProyectoRepositorio;

        private readonly IMapper _mapper;

        public TrabajadorProyectoService(
            ITrabajadorProyectoRepositorio trabajadorProyectoRepositorio,

            IMapper mapper)
        {
            _trabajadorProyectoRepositorio = trabajadorProyectoRepositorio;

            _mapper = mapper;
        }

        public async Task<PaginadoResponse<TrabajadorProyectoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _trabajadorProyectoRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<TrabajadorProyectoDto>>(response.Data);

            return new PaginadoResponse<TrabajadorProyectoDto>(data, response.Meta);
        }


        public async Task<OperationResult<TrabajadorProyectoDto>> CreateAsync(TrabajadorProyectoSaveDto saveDto)
        {
            var trabajadorProyecto = _mapper.Map<TrabajadorProyecto>(saveDto);

            trabajadorProyecto.FechaCreacion = DateTime.Now;

            await _trabajadorProyectoRepositorio.SaveAsync(trabajadorProyecto);

            return new OperationResult<TrabajadorProyectoDto>()
            {
                Data = _mapper.Map<TrabajadorProyectoDto>(trabajadorProyecto),
                Message = "Se ha Creado",
            };

        }

        public async Task<OperationResult<TrabajadorProyectoDto>> DisabledAsync(int id)
        {
            var trabajadorProyecto = await _trabajadorProyectoRepositorio.FindByIdAsync(id);

            if (trabajadorProyecto == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            trabajadorProyecto.Estado = trabajadorProyecto.Estado == 1 ? 0 : 1;


            await _trabajadorProyectoRepositorio.SaveAsync(trabajadorProyecto);

            return new OperationResult<TrabajadorProyectoDto>()
            {
                Data = _mapper.Map<TrabajadorProyectoDto>(trabajadorProyecto),
                Message = trabajadorProyecto.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<TrabajadorProyectoDto>> EditAsync(int id, TrabajadorProyectoSaveDto saveDto)
        {
            var trabajadorProyecto = await _trabajadorProyectoRepositorio.FindByIdAsync(id);

            if (trabajadorProyecto == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, trabajadorProyecto);

            await _trabajadorProyectoRepositorio.SaveAsync(trabajadorProyecto);

            return new OperationResult<TrabajadorProyectoDto>()
            {
                Data = _mapper.Map<TrabajadorProyectoDto>(trabajadorProyecto),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<TrabajadorProyectoDto>> FindAllAsync()
        {
            var response = await _trabajadorProyectoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TrabajadorProyectoDto>>(response);
        }

        public async Task<TrabajadorProyectoDto> FindByIdAsync(int id)
        {
            var response = await _trabajadorProyectoRepositorio.FindByIdAsync(id);

            return _mapper.Map<TrabajadorProyectoDto>(response);
        }

        public async Task<IReadOnlyList<TrabajadorProyectoSelectDto>> SelectActivo()
        {
            var response = await _trabajadorProyectoRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<TrabajadorProyectoSelectDto>>(response);
        }

    }
}