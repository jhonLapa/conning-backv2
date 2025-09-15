using Application.Exceptions;
using Application.Mantenedores.Dtos.Proyectos;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepositorio _proyectoRepositorio;
        private readonly IMapper _mapper;

        public ProyectoService(IProyectoRepositorio proyectoRepositorio, IMapper mapper)
        {
            _proyectoRepositorio = proyectoRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<ProyectoDto>> CreateAsync(ProyectoSaveDto saveDto)
        {
            var proyecto = _mapper.Map<Proyecto>(saveDto);
            proyecto.AuditCreateDate = DateTime.Now;
            proyecto.AuditCreateUser = 1;

            await _proyectoRepositorio.SaveAsync(proyecto);

            return new OperationResult<ProyectoDto>()
            {
                Data = _mapper.Map<ProyectoDto>(proyecto),
                Message = "Proyecto Creado con Exito",
                State = true
            };
        }

        public async Task<OperationResult<ProyectoDto>> DisabledAsync(int id)
        {
            var proyecto = await _proyectoRepositorio.FindByIdAsync(id);

            if (proyecto == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            proyecto.State = !proyecto.State;
            proyecto.AuditDeleteDate = DateTime.Now;

            return new OperationResult<ProyectoDto>()
            {
                Data = _mapper.Map<ProyectoDto>(proyecto),
                Message = proyecto.State
                            ? "Proyecto Activado con éxito"
                            : "Proyecto Desactivado con éxito",
                State = true
            };

        }

        public async Task<OperationResult<ProyectoDto>> EditAsync(int id, ProyectoSaveDto saveDto)
        {
            var proyecto = await _proyectoRepositorio.FindByIdAsync(id);

            if (proyecto == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            proyecto.AuditUpdateDate = DateTime.Now;
            proyecto.AuditCreateUser = 1;

            _mapper.Map(saveDto, proyecto);

            await _proyectoRepositorio.SaveAsync(proyecto);

            return new OperationResult<ProyectoDto>()
            {
                Data = _mapper.Map<ProyectoDto>(proyecto),
                Message = "Proyecto actualizado con exito",
                State = true
            };

        }

        public async Task<IReadOnlyList<ProyectoDto>> FindAllAsync()
        {
            var response = await _proyectoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ProyectoDto>>(response);
        }

        public async Task<ProyectoDto> FindByIdAsync(int id)
        {
            var proyecto = await _proyectoRepositorio.FindByIdAsync(id);

            if (proyecto == null) throw new NotFoundCoreException("Registro no encontrado con ese id");
            
            return _mapper.Map<ProyectoDto>(proyecto);
        }
    }
}
