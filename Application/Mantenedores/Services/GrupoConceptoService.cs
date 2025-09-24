

using Application.Exceptions;
using Application.Mantenedores.Dtos.GrupoConceptos;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class GrupoConceptoService : IGrupoConceptoService
    {
        private readonly IGrupoConceptoRepositorio _grupoConceptoRepositorio;
        private readonly IMapper _mapper;

        public GrupoConceptoService(IGrupoConceptoRepositorio GrupoConceptoRepositorio, IMapper mapper)
        {
            _grupoConceptoRepositorio = GrupoConceptoRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<GrupoConceptoDto>> CreateAsync(GrupoConceptoSaveDto saveDto)
        {
            var grupoConcepto = _mapper.Map<GrupoConcepto>(saveDto);
            grupoConcepto.FechaCreacion = DateTime.Now;
            grupoConcepto.IdUsuarioCreacion = 1;
            grupoConcepto.Estado = 1;

            await _grupoConceptoRepositorio.SaveAsync(grupoConcepto);

            return new OperationResult<GrupoConceptoDto>()
            {
                Data = _mapper.Map<GrupoConceptoDto>(grupoConcepto),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<GrupoConceptoDto>> DisabledAsync(int id)
        {
            var grupoConcepto = await _grupoConceptoRepositorio.FindByIdAsync(id);

            if (grupoConcepto == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            grupoConcepto.Estado = grupoConcepto.Estado == 1 ? 0 : 1;
            grupoConcepto.FechaModificacion = DateTime.Now;

            return new OperationResult<GrupoConceptoDto>()
            {
                Data = _mapper.Map<GrupoConceptoDto>(grupoConcepto),
                Message = grupoConcepto.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<GrupoConceptoDto>> EditAsync(int id, GrupoConceptoSaveDto saveDto)
        {
            var grupoConcepto = await _grupoConceptoRepositorio.FindByIdAsync(id);

            if (grupoConcepto == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            grupoConcepto.FechaModificacion = DateTime.Now;
            grupoConcepto.IdUsuarioModificacion = 1;

            _mapper.Map(saveDto, grupoConcepto);

            await _grupoConceptoRepositorio.SaveAsync(grupoConcepto);

            return new OperationResult<GrupoConceptoDto>()
            {
                Data = _mapper.Map<GrupoConceptoDto>(grupoConcepto),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<GrupoConceptoDto>> FindAllAsync()
        {
            var response = await _grupoConceptoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<GrupoConceptoDto>>(response);
        }

        public async Task<GrupoConceptoDto> FindByIdAsync(int id)
        {
            var grupoConcepto = await _grupoConceptoRepositorio.FindByIdAsync(id);

            if (grupoConcepto == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<GrupoConceptoDto>(grupoConcepto);
        }

        public async Task<IReadOnlyList<GrupoConceptoSelectDto>> SelectGrupoConcepto()
        {
            var response = await _grupoConceptoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<GrupoConceptoSelectDto>>(response);
        }
    }
}