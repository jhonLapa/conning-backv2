using Application.ConceptoAfectacions.Dto;
using Application.ConceptoAfectacions.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.ConceptoAfectacion.Services
{
    public class ConceptoAfectacionService : IConceptoAfectacionServices
    {
        private readonly IConceptoAfectacionRepositorio _conceptoAfectacion;
        private readonly IMapper _mapper;

        public ConceptoAfectacionService(IConceptoAfectacionRepositorio ConceptoAfectacionRepositorio, IMapper mapper)
        {
            _conceptoAfectacion = ConceptoAfectacionRepositorio;
            _mapper = mapper;
        }


        public async Task<OperationResult<IEnumerable<ConceptoAfectacionDto>>> SaveArrayAsync(IEnumerable<ConceptoAfectacionSaveDto> saveDtos)
        {
            var resultList = new List<ConceptoAfectacionDto>();

            foreach (var dto in saveDtos)
            {
                var entity = _mapper.Map<Domain.ConceptoAfectacion>(dto);

                var existing = await _conceptoAfectacion
                                            .FindByConceptoAndAfectacionAsync(dto.IdConcepto, dto.IdAfectacion);

                if (existing != null)
                {
                    _mapper.Map(dto, existing);
                    existing.FechaModificacion = DateTime.Now;
                    existing.IdUsuarioModificacion = 1;
                    entity.Estado = dto.Estado;

                    await _conceptoAfectacion.SaveAsync(existing);

                    // 👇 Agregar al resultado
                    resultList.Add(_mapper.Map<ConceptoAfectacionDto>(existing));
                }
                else
                {
                    entity.FechaCreacion = DateTime.Now;
                    entity.IdUsuarioCreacion = 1;
                    entity.Estado = dto.Estado;

                    await _conceptoAfectacion.SaveAsync(entity);

                    // 👇 Agregar al resultado
                    resultList.Add(_mapper.Map<ConceptoAfectacionDto>(entity));
                }
            }

            return new OperationResult<IEnumerable<ConceptoAfectacionDto>>()
            {
                Data = resultList,
                Message = "Configuraciones procesadas correctamente",
                Success = true
            };
        }
        public async Task<OperationResult<ConceptoAfectacionDto>> CreateAsync(ConceptoAfectacionSaveDto saveDto)
        {
            var ConceptoAfectacion = _mapper.Map<Domain.ConceptoAfectacion>(saveDto);


            await _conceptoAfectacion.SaveAsync(ConceptoAfectacion);

            return new OperationResult<ConceptoAfectacionDto>()
            {
                Data = _mapper.Map<ConceptoAfectacionDto>(ConceptoAfectacion),
                Message = "Se ha Creado",
                Success = true,
            };

        }

        public async Task<OperationResult<ConceptoAfectacionDto>> DisabledAsync(int id)
        {
            var ConceptoAfectacions = await _conceptoAfectacion.FindByIdAsync(id);
            if (ConceptoAfectacions == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            return new OperationResult<ConceptoAfectacionDto>()
            {
                Data = _mapper.Map<ConceptoAfectacionDto>(ConceptoAfectacions),
                Message = "Se ha Desactivado",
                Success = true,
            };
        }

        public async Task<OperationResult<ConceptoAfectacionDto>> EditAsync(int id, ConceptoAfectacionSaveDto saveDto)
        {
            var ConceptoAfectacions = await _conceptoAfectacion.FindByIdAsync(id);

            if (ConceptoAfectacions == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, ConceptoAfectacions);

            await _conceptoAfectacion.SaveAsync(ConceptoAfectacions);

            return new OperationResult<ConceptoAfectacionDto>()
            {
                Data = _mapper.Map<ConceptoAfectacionDto>(ConceptoAfectacions),
                Message = "Se ha actualizado",
                Success = true,
            };

        }

        public async Task<IReadOnlyList<ConceptoAfectacionDto>> FindAllAsync()
        {
            var response = await _conceptoAfectacion.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ConceptoAfectacionDto>>(response);
        }

        public async Task<ConceptoAfectacionDto> FindByIdAsync(int id)
        {
            var response = await _conceptoAfectacion.FindByIdAsync(id);

            return _mapper.Map<ConceptoAfectacionDto>(response);
        }
    }
}