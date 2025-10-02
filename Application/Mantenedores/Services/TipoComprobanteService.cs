using Application.Exceptions;
using Application.Mantenedores.Dtos.Afectacions;
using Application.Mantenedores.Dtos.TiposComprobantes;
using Application.Mantenedores.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Application.Mantenedores.Services
{
    public class TipoComprobanteService : ITipoComprobanteService
    {
        private readonly ITipoComprobanteRepositorio _tipoComprobanteRepositorio;
        private readonly IMapper _mapper;

        public TipoComprobanteService(ITipoComprobanteRepositorio TipoComprobanteRepositorio, IMapper mapper)
        {
            _tipoComprobanteRepositorio = TipoComprobanteRepositorio;
            _mapper = mapper;
        }

        public async Task<PaginadoResponse<TipoComprobanteDto>> BusquedaPaginado(PaginationRequest dto)
        {
            var response = await _tipoComprobanteRepositorio.BusquedaPaginado(dto);

            var data = _mapper.Map<ICollection<TipoComprobanteDto>>(response.Data);

            return new PaginadoResponse<TipoComprobanteDto>(data, response.Meta);
        }
        public async Task<OperationResult<TipoComprobanteDto>> CreateAsync(TipoComprobanteSaveDto saveDto)
        {
            var tipoComprobante = _mapper.Map<TipoComprobante>(saveDto);
            tipoComprobante.FechaCreacion = DateTime.Now;
            tipoComprobante.IdUsuarioCreacion = 1;
            tipoComprobante.Estado = 1;

            await _tipoComprobanteRepositorio.SaveAsync(tipoComprobante);

            return new OperationResult<TipoComprobanteDto>()
            {
                Data = _mapper.Map<TipoComprobanteDto>(tipoComprobante),
                Message = "Creado con Exito",
                Success = true
            };
        }

        public async Task<OperationResult<TipoComprobanteDto>> DisabledAsync(int id)
        {
            var tipoComprobante = await _tipoComprobanteRepositorio.FindByIdAsync(id);

            if (tipoComprobante == null) throw new NotFoundCoreException("Registro no encontrado con ese Id");

            tipoComprobante.Estado = tipoComprobante.Estado == 1 ? 0 : 1;
            tipoComprobante.FechaModificacion = DateTime.Now;

            await _tipoComprobanteRepositorio.SaveAsync(tipoComprobante);

            return new OperationResult<TipoComprobanteDto>()
            {
                Data = _mapper.Map<TipoComprobanteDto>(tipoComprobante),
                Message = tipoComprobante.Estado == 1
                ? "Activado con éxito"
                            : "Desactivado con éxito",
                Success = true
            };

        }

        public async Task<OperationResult<TipoComprobanteDto>> EditAsync(int id, TipoComprobanteSaveDto saveDto)
        {
            var tipoComprobante = await _tipoComprobanteRepositorio.FindByIdAsync(id);

            if (tipoComprobante == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            tipoComprobante.FechaModificacion = DateTime.Now;
            tipoComprobante.IdUsuarioModificacion = 1;

            _mapper.Map(saveDto, tipoComprobante);

            await _tipoComprobanteRepositorio.SaveAsync(tipoComprobante);

            return new OperationResult<TipoComprobanteDto>()
            {
                Data = _mapper.Map<TipoComprobanteDto>(tipoComprobante),
                Message = "actualizado con exito",
                Success = true
            };

        }

        public async Task<IReadOnlyList<TipoComprobanteDto>> FindAllAsync()
        {
            var response = await _tipoComprobanteRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<TipoComprobanteDto>>(response);
        }

        public async Task<TipoComprobanteDto> FindByIdAsync(int id)
        {
            var tipoComprobante = await _tipoComprobanteRepositorio.FindByIdAsync(id);

            if (tipoComprobante == null) throw new NotFoundCoreException("Registro no encontrado con ese id");

            return _mapper.Map<TipoComprobanteDto>(tipoComprobante);
        }
    }
}
