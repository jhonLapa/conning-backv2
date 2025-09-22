using Application.Exceptions;
using Application.Mantenedores.Dtos.DocumentTypes;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class DocumentTypeServices : IDocumentTypeServices
    {
        private readonly IDocumentoRepositorio _documentoRepositorio;
        private readonly IMapper _mapper;

        public DocumentTypeServices(IDocumentoRepositorio documentoRepositorio, IMapper mapper)
        {
            _documentoRepositorio = documentoRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<DocumentTypeDto>> CreateAsync(DocumentTypeSaveDto saveDto)
        {
            var documento =  _mapper.Map<DocumentType>(saveDto);
            documento.FechaCreacion = DateTime.Now;

            var response = await _documentoRepositorio.SaveAsync(documento);

            return new OperationResult<DocumentTypeDto>()
            {
                Data = _mapper.Map<DocumentTypeDto>(documento),
                Message = "Documento Creado Con Exito",
                State = true
            };

        }

        public async Task<OperationResult<DocumentTypeDto>> DisabledAsync(int id)
        {
            var documento = await _documentoRepositorio.FindByIdAsync(id);
            documento.Estado = documento.Estado == 1 ? 0 : 1;

            return new OperationResult<DocumentTypeDto>
            {
                Data = _mapper.Map<DocumentTypeDto>(documento),
                Message = documento.Estado == 1
                        ? "Activado con éxito"
                        : "Desactivado con éxito",
                State = true
            };

        }

        public async Task<OperationResult<DocumentTypeDto>> EditAsync(int id, DocumentTypeSaveDto saveDto)
        {
            var documento = await _documentoRepositorio.FindByIdAsync(id);
            documento.FechaModificacion = DateTime.Now;

            if (documento == null) throw new NotFoundCoreException("Documento no encontrado con ese id");

            _mapper.Map(saveDto, documento);

            await _documentoRepositorio.SaveAsync(documento);

            return new OperationResult<DocumentTypeDto>()
            {
                Data = _mapper.Map<DocumentTypeDto>(documento),
                Message = "Documento Actualizado Con Exito",
                State = true
            };

        }

        public async Task<IReadOnlyList<DocumentTypeDto>> FindAllAsync()
        {
            var response = await _documentoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DocumentTypeDto>>(response);
        }

        public async Task<DocumentTypeDto> FindByIdAsync(int id)
        {
            var response = await _documentoRepositorio.FindByIdAsync(id);

            return _mapper.Map<DocumentTypeDto>(response);
        }
    }
}
