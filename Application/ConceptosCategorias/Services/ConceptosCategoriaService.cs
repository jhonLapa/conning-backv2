using Application.ConceptosCategorias.Dto;
using Application.ConceptosCategorias.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.ConceptosCategorias.Servicess
{
    public class ConceptosCategoriaService : IConceptosCategoriaServices
    {
        private readonly IConceptosCategoriaRepositorio _conceptosCategoriaRepositorio;

        private readonly IMapper _mapper;

        public ConceptosCategoriaService(
            IConceptosCategoriaRepositorio conceptosCategoriaRepositorio,

            IMapper mapper)
        {
            _conceptosCategoriaRepositorio = conceptosCategoriaRepositorio;

            _mapper = mapper;
        }

        public async Task<PaginadoResponse<ConceptosCategoriaDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _conceptosCategoriaRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<ConceptosCategoriaDto>>(response.Data);

            return new PaginadoResponse<ConceptosCategoriaDto>(data, response.Meta);
        }


        public async Task<OperationResult<ConceptosCategoriaDto>> CreateAsync(ConceptosCategoriaSaveDto saveDto)
        {
            var conceptosCategoria = _mapper.Map<ConceptosCategoria>(saveDto);

            conceptosCategoria.FechaCreacion = DateTime.Now;
            conceptosCategoria.Estado =1;
            conceptosCategoria.UsuarioCreacion = "Admin";

            await _conceptosCategoriaRepositorio.SaveAsync(conceptosCategoria);

            return new OperationResult<ConceptosCategoriaDto>()
            {
                Data = _mapper.Map<ConceptosCategoriaDto>(conceptosCategoria),
                Message = "Se ha Creado",
            };

        }

        public async Task<OperationResult<ConceptosCategoriaDto>> DisabledAsync(int id)
        {
            var conceptosCategoria = await _conceptosCategoriaRepositorio.FindByIdAsync(id);

            if (conceptosCategoria == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            conceptosCategoria.Estado = conceptosCategoria.Estado == 1 ? 0 : 1;


            await _conceptosCategoriaRepositorio.SaveAsync(conceptosCategoria);

            return new OperationResult<ConceptosCategoriaDto>()
            {
                Data = _mapper.Map<ConceptosCategoriaDto>(conceptosCategoria),
                Message = conceptosCategoria.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<ConceptosCategoriaDto>> EditAsync(int id, ConceptosCategoriaSaveDto saveDto)
        {
            var conceptosCategoria = await _conceptosCategoriaRepositorio.FindByIdAsync(id);

            if (conceptosCategoria == null) throw new NotFoundCoreException("Registro no encontrado con el id");


            conceptosCategoria.UsuarioCreacion = "Admin";

            _mapper.Map(saveDto, conceptosCategoria);

            await _conceptosCategoriaRepositorio.SaveAsync(conceptosCategoria);

            return new OperationResult<ConceptosCategoriaDto>()
            {
                Data = _mapper.Map<ConceptosCategoriaDto>(conceptosCategoria),
                Message = "Se ha actualizado",
            };

        }

        public async Task<IReadOnlyList<ConceptosCategoriaDto>> FindAllAsync()
        {
            var response = await _conceptosCategoriaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ConceptosCategoriaDto>>(response);
        }

        public async Task<ConceptosCategoriaDto> FindByIdAsync(int id)
        {
            var response = await _conceptosCategoriaRepositorio.FindByIdAsync(id);

            return _mapper.Map<ConceptosCategoriaDto>(response);
        }

        public async Task<IReadOnlyList<ConceptosCategoriaSelectDto>> SelectActivo()
        {
            var response = await _conceptosCategoriaRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<ConceptosCategoriaSelectDto>>(response);
        }

    }
}