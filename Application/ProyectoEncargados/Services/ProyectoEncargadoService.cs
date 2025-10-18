using Application.Exceptions;
using Application.ProyectoEncargados.Dto;
using Application.ProyectoEncargados.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.ProyectoEncargados.Servicess
{
    public class ProyectoEncargadoService : IProyectoEncargadoServices
    {
        private readonly IProyectoEncargadoRepositorio _proyectoEncargadoRepositorio;

        private readonly IMapper _mapper;

        public ProyectoEncargadoService(
            IProyectoEncargadoRepositorio proyectoEncargadoRepositorio,

            IMapper mapper)
        {
            _proyectoEncargadoRepositorio = proyectoEncargadoRepositorio;

            _mapper = mapper;
        }

        public async Task<PaginadoResponse<ProyectoEncargadoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _proyectoEncargadoRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<ProyectoEncargadoDto>>(response.Data);

            return new PaginadoResponse<ProyectoEncargadoDto>(data, response.Meta);
        }


        public async Task<OperationResult<ProyectoEncargadoDto>> CreateAsync(ProyectoEncargadoSaveDto saveDto)
        {
            var proyectoEncargado = _mapper.Map<ProyectoEncargado>(saveDto);

            proyectoEncargado.FechaInicio = DateTime.Now;

            await _proyectoEncargadoRepositorio.SaveAsync(proyectoEncargado);

            return new OperationResult<ProyectoEncargadoDto>()
            {
                Data = _mapper.Map<ProyectoEncargadoDto>(proyectoEncargado),
                Message = "Se ha Creado",
            };

        }

        

        public async Task<OperationResult<ProyectoEncargadoDto>> DisabledAsync(int id)
        {
            var proyectoEncargado = await _proyectoEncargadoRepositorio.FindByIdAsync(id);

            if (proyectoEncargado == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            proyectoEncargado.Estado = proyectoEncargado.Estado == 1 ? 0 : 1;
            

            await _proyectoEncargadoRepositorio.SaveAsync(proyectoEncargado);

            return new OperationResult<ProyectoEncargadoDto>()
            {
                Data = _mapper.Map<ProyectoEncargadoDto>(proyectoEncargado),
                Message = proyectoEncargado.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<ProyectoEncargadoDto>> EditAsync(int id, ProyectoEncargadoSaveDto saveDto)
        {
            var proyectoEncargado = await _proyectoEncargadoRepositorio.FindByIdAsync(id);

            if (proyectoEncargado == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, proyectoEncargado);

            await _proyectoEncargadoRepositorio.SaveAsync(proyectoEncargado);

            return new OperationResult<ProyectoEncargadoDto>()
            {
                Data = _mapper.Map<ProyectoEncargadoDto>(proyectoEncargado),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<ProyectoEncargadoDto>> FindAllAsync()
        {
            var response = await _proyectoEncargadoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ProyectoEncargadoDto>>(response);
        }

        public async Task<ProyectoEncargadoDto> FindByIdAsync(int id)
        {
            var response = await _proyectoEncargadoRepositorio.FindByIdAsync(id);

            return _mapper.Map<ProyectoEncargadoDto>(response);
        }

        public async Task<IReadOnlyList<ProyectoEncargadoSelectDto>> SelectActivo()
        {
            var response = await _proyectoEncargadoRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<ProyectoEncargadoSelectDto>>(response);
        }

    }
}
