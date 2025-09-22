using Application.Exceptions;
using Application.Mantenedores.Dtos.Afectacions;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class AfectacionService : IAfectacionService
    {
        private readonly IAfectacionRepositorio _afectacionRepositorio;
        private readonly IMapper _mapper;

        public AfectacionService(IAfectacionRepositorio AfectacionRepositorio, IMapper mapper)
        {
            _afectacionRepositorio = AfectacionRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<AfectacionDto>> CreateAsync(AfectacionSaveDto saveDto)
        {
            var afectacion = _mapper.Map<Afectacion>(saveDto);
            afectacion.FechaCreacion = DateTime.Now;
            afectacion.IdUsuarioCreacion = 1;

            await _afectacionRepositorio.SaveAsync(afectacion);

            return new OperationResult<AfectacionDto>()
            {
                Data = _mapper.Map<AfectacionDto>(afectacion),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<AfectacionDto>> DisabledAsync(int id)
        {
            var afectacion = await _afectacionRepositorio.FindByIdAsync(id);

            if (afectacion == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            afectacion.Estado = afectacion.Estado == 1 ? 0 : 1;
            afectacion.FechaModificacion = DateTime.Now;

            return new OperationResult<AfectacionDto>()
            {
                Data = _mapper.Map<AfectacionDto>(afectacion),
                Message = afectacion.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<AfectacionDto>> EditAsync(int id, AfectacionSaveDto saveDto)
        {
            var afectacion = await _afectacionRepositorio.FindByIdAsync(id);

            if (afectacion == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            afectacion.FechaModificacion = DateTime.Now;
            afectacion.IdUsuarioModificacion = 1;

            _mapper.Map(saveDto, afectacion);

            await _afectacionRepositorio.SaveAsync(afectacion);

            return new OperationResult<AfectacionDto>()
            {
                Data = _mapper.Map<AfectacionDto>(afectacion),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<AfectacionDto>> FindAllAsync()
        {
            var response = await _afectacionRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<AfectacionDto>>(response);
        }

        public async Task<AfectacionDto> FindByIdAsync(int id)
        {
            var afectacion = await _afectacionRepositorio.FindByIdAsync(id);

            if (afectacion == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<AfectacionDto>(afectacion);
        }
    }
}
