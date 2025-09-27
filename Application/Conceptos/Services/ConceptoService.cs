using Application.Conceptos.Dto;
using Application.Conceptos.Services.Interfaces;
using Application.Exceptions;
using Application.Mantenedores.Dtos.Afectacions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.Concepto.Services
{
    public class ConceptoService : IConceptoServices
    {
        private readonly IConceptoRepositorio _conceptoRepositorio;
        private readonly IMapper _mapper;

        public ConceptoService(IConceptoRepositorio conceptoRepositorio, IMapper mapper)
        {
            _conceptoRepositorio = conceptoRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<ConceptoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _conceptoRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<ConceptoDto>>(response.Data);

            return new PaginadoResponse<ConceptoDto>(data, response.Meta);
        }

        public async Task<OperationResult<ConceptoDto>> CreateAsync(ConceptoSaveDto saveDto)
        {
            var concepto = _mapper.Map<Domain.Concepto>(saveDto);
            concepto.FechaCreacion = DateTime.Now;

            concepto.IdUsuarioCreacion = 1;
            concepto.Estado = 1;
            await _conceptoRepositorio.SaveAsync(concepto);

            return new OperationResult<ConceptoDto>()
            {
                Data = _mapper.Map<ConceptoDto>(concepto),
                Message = "Se ha Creado",
                Success = true,
            };

        }

        public async Task<OperationResult<ConceptoDto>> DisabledAsync(int id)
        {
            var conceptos = await _conceptoRepositorio.FindByIdAsync(id);

            if (conceptos == null) throw new NotFoundCoreException("Registro no encontrado con el id");

            conceptos.Estado = conceptos.Estado == 1 ? 0 : 1;

            await _conceptoRepositorio.SaveAsync(conceptos);

            return new OperationResult<ConceptoDto>()
            {
                Data = _mapper.Map<ConceptoDto>(conceptos),
                Message = "Se ha Desactivado",
                Success = true,
            };
        }

        public async Task<OperationResult<ConceptoDto>> EditAsync(int id, ConceptoSaveDto saveDto)
        {
            var conceptos = await _conceptoRepositorio.FindByIdAsync(id);

            if (conceptos == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            _mapper.Map(saveDto, conceptos);

            await _conceptoRepositorio.SaveAsync(conceptos);

            return new OperationResult<ConceptoDto>()
            {
                Data = _mapper.Map<ConceptoDto>(conceptos),
                Message = "Se ha actualizado",
                Success = true,
            };

        }

        public async Task<IReadOnlyList<ConceptoDto>> FecthConceptoByIdGrupo(int idGrupo)
        {
            var response = await _conceptoRepositorio.FecthConceptoByIdGrupo(idGrupo);

            return _mapper.Map<IReadOnlyList<ConceptoDto>>(response);
        }

        public async Task<IReadOnlyList<ConceptoDto>> FindAllAsync()
        {
            var response = await _conceptoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ConceptoDto>>(response);
        }

        public async Task<ConceptoDto> FindByIdAsync(int id)
        {
            var response = await _conceptoRepositorio.FindByIdAsync(id);

            return _mapper.Map<ConceptoDto>(response);
        }
    }
}

