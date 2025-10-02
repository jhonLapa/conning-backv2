using Application.Exceptions;
using Application.Mantenedores.Dtos.TiposDocumento;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly ITipoDocumentoRepositorio _documentoRepositorio;
        private readonly IMapper _mapper;

        public TipoDocumentoService(ITipoDocumentoRepositorio documentoRepositorio, IMapper mapper)
        {
            _documentoRepositorio = documentoRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<TipoDocumentoDto>> CreateAsync(TipoDocumentoSaveDto saveDto)
        {
            var documento =  _mapper.Map<TipoDocumento>(saveDto);
            documento.FechaCreacion = DateTime.Now;
            documento.Estado = 1;

            var response = await _documentoRepositorio.SaveAsync(documento);

            return new OperationResult<TipoDocumentoDto>()
            {
                Data = _mapper.Map<TipoDocumentoDto>(documento),
                Message = "Documento Creado Con Exito",
                Success = true
            };

        }

        public async Task<OperationResult<TipoDocumentoDto>> DisabledAsync(int id)
        {
            var documento = await _documentoRepositorio.FindByIdAsync(id);
            documento.FechaModificacion = DateTime.Now;
            documento.Estado = documento.Estado == 1 ? 0 : 1;

            return new OperationResult<TipoDocumentoDto>
            {
                Data = _mapper.Map<TipoDocumentoDto>(documento),
                Message = documento.Estado == 1
                        ? "Activado con éxito"
                        : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<TipoDocumentoDto>> EditAsync(int id, TipoDocumentoSaveDto saveDto)
        {
            var documento = await _documentoRepositorio.FindByIdAsync(id);
            documento.FechaModificacion = DateTime.Now;

            if (documento == null) throw new NotFoundCoreException("Documento no encontrado con ese id");

            _mapper.Map(saveDto, documento);

            await _documentoRepositorio.SaveAsync(documento);

            return new OperationResult<TipoDocumentoDto>()
            {
                Data = _mapper.Map<TipoDocumentoDto>(documento),
                Message = "Documento Actualizado Con Exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<TipoDocumentoDto>> FindAllAsync()
        {
            var response = await _documentoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TipoDocumentoDto>>(response);
        }

        public async Task<TipoDocumentoDto> FindByIdAsync(int id)
        {
            var response = await _documentoRepositorio.FindByIdAsync(id);

            return _mapper.Map<TipoDocumentoDto>(response);
        }
    }
}
