using Application.Exceptions;
using Application.Mantenedores.Dtos.Bancos;
using Application.Mantenedores.Dtos.TiposDocumento;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
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

        public async Task<PaginadoResponse<TipoDocumentoDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _documentoRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<TipoDocumentoDto>>(response.Data);

            return new PaginadoResponse<TipoDocumentoDto>(data, response.Meta);
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
            var bank = await _documentoRepositorio.FindByIdAsync(id);

            if (bank == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            bank.Estado = bank.Estado == 1 ? 0 : 1;
            bank.FechaModificacion = DateTime.Now;

            await _documentoRepositorio.SaveAsync(bank);

            return new OperationResult<TipoDocumentoDto>()
            {
                Data = _mapper.Map<TipoDocumentoDto>(bank),
                Message = bank.Estado == 1
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

        public async Task<IReadOnlyList<TipoDocumentoSelectDto>> SelectActivo()
        {
            var response = await _documentoRepositorio.SelectActivo();

            return _mapper.Map<IReadOnlyList<TipoDocumentoSelectDto>>(response);
        }
    }
}
