using Application.Exceptions;
using Application.Mantenedores.Dtos.Proyectos;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProyectoRepositorio _projectRepositorio;
        private readonly IMapper _mapper;

        public ProjectService(IProyectoRepositorio ProjectRepositorio, IMapper mapper)
        {
            _projectRepositorio = ProjectRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<ProjectDto>> CreateAsync(ProjectSaveDto saveDto)
        {
            var project = _mapper.Map<Proyecto>(saveDto);
            project.FechaCreacion = DateTime.Now;
            project.Estado = 1;

            await _projectRepositorio.SaveAsync(project);

            return new OperationResult<ProjectDto>()
            {
                Data = _mapper.Map<ProjectDto>(project),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<ProjectDto>> DisabledAsync(int id)
        {
            var project = await _projectRepositorio.FindByIdAsync(id);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            project.Estado = project.Estado == 1 ? 0 : 1;
            project.FechaModificacion = DateTime.Now;

            return new OperationResult<ProjectDto>()
            {
                Data = _mapper.Map<ProjectDto>(project),
                Message = project.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<ProjectDto>> EditAsync(int id, ProjectSaveDto saveDto)
        {
            var project = await _projectRepositorio.FindByIdAsync(id);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            project.FechaModificacion = DateTime.Now;

            _mapper.Map(saveDto, project);

            await _projectRepositorio.SaveAsync(project);

            return new OperationResult<ProjectDto>()
            {
                Data = _mapper.Map<ProjectDto>(project),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<ProjectDto>> FindAllAsync()
        {
            var response = await _projectRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ProjectDto>>(response);
        }

        public async Task<ProjectDto> FindByIdAsync(int id)
        {
            var project = await _projectRepositorio.FindByIdAsync(id);

            if (project == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<ProjectDto>(project);
        }
    }
}
