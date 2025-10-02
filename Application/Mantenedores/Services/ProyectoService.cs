using Application.Exceptions;
using Application.Mantenedores.Dtos.Proyectos;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepositorio _projectRepositorio;
        private readonly IMapper _mapper;

        public ProyectoService(IProyectoRepositorio ProjectRepositorio, IMapper mapper)
        {
            _projectRepositorio = ProjectRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<ProyectoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _projectRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<ProyectoDto>>(response.Data);

            return new PaginadoResponse<ProyectoDto>(data, response.Meta);
        }


        public async Task<OperationResult<ProyectoDto>> CreateAsync(ProyectoSaveDto saveDto)
        {
            var project = _mapper.Map<Proyecto>(saveDto);
            project.FechaCreacion = DateTime.Now;
            project.Estado = 1;

            await _projectRepositorio.SaveAsync(project);

            return new OperationResult<ProyectoDto>()
            {
                Data = _mapper.Map<ProyectoDto>(project),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<ProyectoDto>> DisabledAsync(int id)
        {
            var project = await _projectRepositorio.FindByIdAsync(id);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            project.Estado = project.Estado == 1 ? 0 : 1;
            project.FechaModificacion = DateTime.Now;

            return new OperationResult<ProyectoDto>()
            {
                Data = _mapper.Map<ProyectoDto>(project),
                Message = project.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<ProyectoDto>> EditAsync(int id, ProyectoSaveDto saveDto)
        {
            var project = await _projectRepositorio.FindByIdAsync(id);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            project.FechaModificacion = DateTime.Now;

            _mapper.Map(saveDto, project);

            await _projectRepositorio.SaveAsync(project);

            return new OperationResult<ProyectoDto>()
            {
                Data = _mapper.Map<ProyectoDto>(project),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<ProyectoDto>> FindAllAsync()
        {
            var response = await _projectRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ProyectoDto>>(response);
        }

        public async Task<ProyectoDto> FindByIdAsync(int id)
        {
            var project = await _projectRepositorio.FindByIdAsync(id);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<ProyectoDto>(project);
        }

        public async Task<IReadOnlyList<ProyectoSelectDto>> SelectActivo()
        {
            var response = await _projectRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ProyectoSelectDto>>(response);
        }
    }
}
