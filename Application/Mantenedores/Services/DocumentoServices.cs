using Application.Exceptions;
using Application.Mantenedores.Dtos.Documentos;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Services
{
    public class DocumentoServices : IDocumentoServices
    {
        private readonly IDocumentoRepositorio _documentoRepositorio;
        private readonly IMapper _mapper;

        public DocumentoServices(IDocumentoRepositorio documentoRepositorio, IMapper mapper)
        {
            _documentoRepositorio = documentoRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<DocumentoDto>> CreateAsync(DocumentoSaveDto saveDto)
        {
            var documento =  _mapper.Map<Documento>(saveDto);
            documento.AuditCreateDate = DateTime.Now;

            var response = await _documentoRepositorio.SaveAsync(documento);

            return new OperationResult<DocumentoDto>()
            {
                Data = _mapper.Map<DocumentoDto>(documento),
                Message = "Documento Creado Con Exito",
                State = true
            };

        }

        public async Task<OperationResult<DocumentoDto>> DisabledAsync(int id)
        {
            var documento = await _documentoRepositorio.FindByIdAsync(id);
            documento.State = !documento.State;

            return new OperationResult<DocumentoDto>
            {
                Data = _mapper.Map<DocumentoDto>(documento),
                Message = documento.State
                            ? "Documento Activado con éxito"
                            : "Documento Desactivado con éxito",
                State = true
            };

        }

        public async Task<OperationResult<DocumentoDto>> EditAsync(int id, DocumentoSaveDto saveDto)
        {
            var documento = await _documentoRepositorio.FindByIdAsync(id);
            documento.AuditUpdateDate = DateTime.Now;

            if (documento == null) throw new NotFoundCoreException("Documento no encontrado con ese id");

            _mapper.Map(saveDto, documento);

            await _documentoRepositorio.SaveAsync(documento);

            return new OperationResult<DocumentoDto>()
            {
                Data = _mapper.Map<DocumentoDto>(documento),
                Message = "Documento Actualizado Con Exito",
                State = true
            };

        }

        public async Task<IReadOnlyList<DocumentoDto>> FindAllAsync()
        {
            var response = await _documentoRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DocumentoDto>>(response);
        }

        public async Task<DocumentoDto> FindByIdAsync(int id)
        {
            var response = await _documentoRepositorio.FindByIdAsync(id);

            return _mapper.Map<DocumentoDto>(response);
        }
    }
}
