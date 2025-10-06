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
            // 🚫 Validar duplicado por nombre
            var existe = await _documentoRepositorio.ExistsAsync(d =>
                d.Nombre.ToLower() == saveDto.Nombre.ToLower());

            if (existe)
                throw new NotFoundCoreException("Ya existe un Tipo de Documento con el mismo nombre.");

            // ⚙️ Generar código automáticamente con prefijo "D"
            var codigoGenerado = await _documentoRepositorio.GenerarCodigoAsync("D");

            var documento = new TipoDocumento
            {
                Nombre = saveDto.Nombre,
                Codigo = codigoGenerado,
                FechaCreacion = DateTime.Now,
                Estado = 1
            };

            await _documentoRepositorio.SaveAsync(documento);

            return new OperationResult<TipoDocumentoDto>
            {
                Data = _mapper.Map<TipoDocumentoDto>(documento),
                Message = $"Documento creado con código {codigoGenerado}",
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
            if (documento == null)
                throw new NotFoundCoreException("Documento no encontrado con ese id.");

            // 🚫 Validar duplicado de nombre (excluyendo el mismo ID)
            var existeDuplicado = await _documentoRepositorio.ExistsAsync(d =>
                d.Nombre.ToLower() == saveDto.Nombre.ToLower(), id);

            if (existeDuplicado)
                throw new NotFoundCoreException("Ya existe otro Tipo de Documento con el mismo nombre.");

            // ⚙️ Actualizar datos
            documento.Nombre = saveDto.Nombre;
            documento.FechaModificacion = DateTime.Now;
            documento.UsuarioModificacion = saveDto.UsuarioModificacion ?? "admin";

            // ⚡ Si el código está vacío (caso raro), generar uno nuevo
            if (string.IsNullOrWhiteSpace(documento.Codigo))
                documento.Codigo = await _documentoRepositorio.GenerarCodigoAsync("D");

            await _documentoRepositorio.SaveAsync(documento);

            return new OperationResult<TipoDocumentoDto>
            {
                Data = _mapper.Map<TipoDocumentoDto>(documento),
                Message = "Documento actualizado con éxito.",
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
